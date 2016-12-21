using System;
using System.IO;
using System.Windows.Forms;
using ED_Explorer.Services;

namespace ED_Explorer {
   public partial class Main : Form {
      private static DateTime _lastRead = DateTime.MinValue;
      private static long _lastSize = 0;
      readonly JournalSystemWatcher _journalWatcher = new JournalSystemWatcher();
      public static string Appfolder = Application.StartupPath;
      public static string DbPath = Application.StartupPath + "\\ed-explorer.db";

      public Main() {
         InitializeComponent();
         _journalWatcher.Created += OnJournalCreated;
         _journalWatcher.Changed += OnJournalChanged;
         _journalWatcher.Path = GetPath();
         _journalWatcher.Filter = "*.log";
         _journalWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
           | NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.Size;


         _journalWatcher.EnableRaisingEvents = true;
         AppModel.Settings = SettingsServices.GetSetting(DbPath);
         AppModel.DbPath = DbPath;

      }

      private void OnJournalChanged(object sender, FileSystemEventArgs e) {
         Console.WriteLine(e.Name + @" changed.");
      }

      private void OnJournalCreated(object sender, FileSystemEventArgs e) {
         Console.WriteLine(e.Name + @" created.");
      }

      private string GetPath() {
         return "C:\\Users\\John\\Saved Games\\Frontier Developments\\Elite Dangerous";
      }

      private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
         Application.ExitThread();
      }

      private void profileToolStripMenuItem_Click(object sender, EventArgs e) {
         SettingsForm form = new SettingsForm();
         form.Show();
      }
   }
}
