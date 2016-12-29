using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ED_Explorer.Models.DataModels;
using ED_Explorer.Services;

namespace ED_Explorer {
   public partial class Main : Form {
      private static DateTime _lastRead = DateTime.MinValue;
      private static long _lastSize = 0;
      readonly JournalSystemWatcher _journalWatcher = new JournalSystemWatcher();
      ConcurrentQueue<string> fileQueue;
      public static string Appfolder = Application.StartupPath;
      public static string DbPath = Application.StartupPath + "\\ed-explorer.db";
      public delegate void NewLogEntry(string txt, Color c);
      public event NewLogEntry OnNewLogEntry;
      private string logtext = "";     // to keep in case of no logs..
      private FileService _fileService;
      // private ICollection<JournalFiles> _journalFiles;  

      public string LogText { get { return logtext; } }

      public Main() {
         InitializeComponent();

         _journalWatcher.Created += OnJournalCreated;
         _journalWatcher.Changed += OnJournalChanged;
         _journalWatcher.Path = GetPath();
         _journalWatcher.Filter = "*.log";
         _journalWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
           | NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.Size;

         fileQueue = new ConcurrentQueue<string>();

         _journalWatcher.EnableRaisingEvents = true;

         OnNewLogEntry += AppendText;
      }

      public void AppendText(string s, Color c) {
         richTextBoxScroll1.AppendText(s, c);
      }

      public void LogLine(string text) {
         LogLineColor(text, Color.Black);
      }

      public void LogLineColor(string text, Color color) {
         try {
            Invoke((MethodInvoker)delegate {
               logtext += text + Environment.NewLine;      // keep this, may be the only log showing

               if (OnNewLogEntry != null)
                  OnNewLogEntry(text + Environment.NewLine, color);
            });
         } catch { }
      }

      private void OnJournalChanged(object sender, FileSystemEventArgs e) {

         LogLine(e.Name + @" changed.");
         Console.WriteLine(e.Name + @" changed.");
      }

      private void OnJournalCreated(object sender, FileSystemEventArgs e) {
         _fileService = new FileService(e.FullPath);
         LogLine(e.Name + @" created.");
         Console.WriteLine(e.Name + @" created.");
      }

      private string GetPath() {
         return "C:\\Users\\John\\Saved Games\\Frontier Developments\\Elite Dangerous";
      }

      private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
         Application.ExitThread();
      }

      private void profileToolStripMenuItem_Click(object sender, EventArgs e) {
         ShowSettingsForm();
      }

      private void ShowSettingsForm() {
         var form = new SettingsForm();
         if (form.ShowDialog() == DialogResult.OK) {
            UserLogin();
         }
      }

      private void authenticateToolStripMenuItem_Click(object sender, EventArgs e) {
         Authenticate.Login();
      }

      private void Main_Load(object sender, EventArgs e) {
         LogLine("Loading...");
         AppModel.DbPath = DbPath;
         if (File.Exists(DbPath)) {
            GetSettings();
            UserLogin();
         } else {
            ShowSettingsForm();
            GetSettings();

         }
         if (AppModel.Files.Count == 0) {
            LoadJournals();
         }
      }

      private void LoadJournals() {
         
      }

      private void GetSettings() {
         LogLine("Get Settings...");
         AppModel.Settings = SettingsServices.GetSetting(DbPath);
         LogLine("Get Settings...Ok");
      }

      private void UserLogin() {
         LogLine("Communicating with server...");
         Authenticate.Login();
         UserService.GetUserData();
         LogLine("Communicating with server...Ok");
      }
   }
}
