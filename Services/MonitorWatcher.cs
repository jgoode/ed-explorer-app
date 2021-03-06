﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ED_Explorer.Services {
   public class MonitorWatcher {
      public delegate void NewJournalEntryHandler(JournalEntry je);
      public event NewJournalEntryHandler OnNewJournalEntry;

      public string m_watcherfolder;

      Dictionary<string, EDJournalReader> netlogreaders = new Dictionary<string, EDJournalReader>();
      EDJournalReader lastnfi = null;          // last one read..
      FileSystemWatcher m_Watcher;
      System.Windows.Forms.Timer m_scantimer;
      System.ComponentModel.BackgroundWorker m_worker;
      private int ticksNoActivity = 0;
      ConcurrentQueue<string> m_netLogFileQueue;

      public MonitorWatcher(string folder) {
         m_watcherfolder = folder;
      }

      public void ParseJournalFiles(Func<bool> cancelRequested, Action<int, string> updateProgress, bool forceReload = false) {
         System.Diagnostics.Trace.WriteLine("Scanned " + m_watcherfolder);

         Dictionary<string, TravelLogUnit> m_travelogUnits = TravelLogUnit.GetAll().Where(t => (t.type & 0xFF) == 3).GroupBy(t => t.Name).Select(g => g.First()).ToDictionary(t => t.Name);

         // order by file write time so we end up on the last one written
         FileInfo[] allFiles = Directory.EnumerateFiles(m_watcherfolder, "Journal.*.log", SearchOption.AllDirectories).Select(f => new FileInfo(f)).OrderBy(p => p.LastWriteTime).ToArray();

         List<EDJournalReader> readersToUpdate = new List<EDJournalReader>();

         for (int i = 0; i < allFiles.Length; i++) {
            FileInfo fi = allFiles[i];

            var reader = OpenFileReader(fi, m_travelogUnits);

            if (!m_travelogUnits.ContainsKey(reader.TravelLogUnit.Name)) {
               m_travelogUnits[reader.TravelLogUnit.Name] = reader.TravelLogUnit;
               reader.TravelLogUnit.type = 3;
               reader.TravelLogUnit.Add();
            }

            if (!netlogreaders.ContainsKey(reader.TravelLogUnit.Name)) {
               netlogreaders[reader.TravelLogUnit.Name] = reader;
            }

            if (forceReload) {
               // Force a reload of the travel log
               reader.TravelLogUnit.Size = 0;
            }

            if (reader.filePos != fi.Length || i == allFiles.Length - 1)  // File not already in DB, or is the last one
            {
               readersToUpdate.Add(reader);
            }
         }

         for (int i = 0; i < readersToUpdate.Count; i++) {
            using (SQLiteConnectionUser cn = new SQLiteConnectionUser(utc: true)) {
               EDJournalReader reader = readersToUpdate[i];
               updateProgress(i * 100 / readersToUpdate.Count, reader.TravelLogUnit.Name);

               List<JournalEntry> entries = reader.ReadJournalLog().ToList();      // this may create new commanders, and may write to the TLU db

               using (DbTransaction tn = cn.BeginTransaction()) {
                  foreach (JournalEntry je in entries) {
                     System.Diagnostics.Trace.WriteLine(string.Format("Write Journal to db {0} {1}", je.EventTimeUTC, je.EventTypeStr));
                     je.Add(cn, tn);
                  }

                  tn.Commit();
               }

               reader.TravelLogUnit.Update(cn);

               updateProgress((i + 1) * 100 / readersToUpdate.Count, reader.TravelLogUnit.Name);

               lastnfi = reader;
            }
         }

         updateProgress(-1, "");
      }

      private EDJournalReader OpenFileReader(FileInfo fi, Dictionary<string, TravelLogUnit> tlu_lookup = null) {
         EDJournalReader reader;
         TravelLogUnit tlu;

         System.Diagnostics.Trace.WriteLine(string.Format("File Read {0}", fi.FullName));

         if (netlogreaders.ContainsKey(fi.Name)) {
            reader = netlogreaders[fi.Name];
         } else if (tlu_lookup != null && tlu_lookup.ContainsKey(fi.Name)) {
            tlu = tlu_lookup[fi.Name];
            tlu.Path = fi.DirectoryName;
            reader = new EDJournalReader(tlu);
            netlogreaders[fi.Name] = reader;
         } else if (TravelLogUnit.TryGet(fi.Name, out tlu)) {
            tlu.Path = fi.DirectoryName;
            reader = new EDJournalReader(tlu);
            netlogreaders[fi.Name] = reader;
         } else {
            reader = new EDJournalReader(fi.FullName);

#if false
                // Bring over the commander from the previous log if possible
                Match match = journalNamePrefixRe.Match(fi.Name);
                if (match.Success)
                {
                    string prefix = match.Groups["prefix"].Value;
                    string partstr = match.Groups["part"].Value;
                    int part;
                    if (Int32.TryParse(partstr, NumberStyles.Integer, CultureInfo.InvariantCulture, out part) && part > 1)
                    {
                        //EDCommander lastcmdr = EDDConfig.Instance.CurrentCommander;
                        var lastreader = netlogreaders.Where(kvp => kvp.Key.StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
                                                      .Select(k => k.Value)
                                                      .FirstOrDefault();
                        //if (lastreader != null)
                        //{
                            //lastcmdr = lastreader.Commander;
                        //}

                        //reader.Commander = lastcmdr;
                    }
                }
#endif
            netlogreaders[fi.Name] = reader;
         }

         return reader;
      }

      public void StartMonitor() {
         Debug.Assert(Application.MessageLoop);              // ensure.. paranoia

         if (m_Watcher == null) {
            try {
               m_netLogFileQueue = new ConcurrentQueue<string>();
               m_Watcher = new System.IO.FileSystemWatcher();
               m_Watcher.Path = m_watcherfolder + Path.DirectorySeparatorChar;
               m_Watcher.Filter = "Journal.*.log";
               m_Watcher.IncludeSubdirectories = false;
               m_Watcher.NotifyFilter = NotifyFilters.FileName;
               m_Watcher.Changed += new FileSystemEventHandler(OnNewFile);
               m_Watcher.Created += new FileSystemEventHandler(OnNewFile);
               m_Watcher.EnableRaisingEvents = true;

               m_worker = new System.ComponentModel.BackgroundWorker();
               m_worker.DoWork += ScanTickWorker;
               m_worker.RunWorkerCompleted += ScanTickDone;
               m_worker.WorkerSupportsCancellation = true;

               m_scantimer = new System.Windows.Forms.Timer();
               m_scantimer.Interval = 2000;
               m_scantimer.Tick += ScanTick;
               m_scantimer.Start();

               System.Diagnostics.Trace.WriteLine("Start Monitor on " + m_watcherfolder);
            } catch (Exception ex) {
               System.Windows.Forms.MessageBox.Show("Start Monitor exception : " + ex.Message, "EDDiscovery Error");
               System.Diagnostics.Trace.WriteLine("Start Monitor exception : " + ex.Message);
               System.Diagnostics.Trace.WriteLine(ex.StackTrace);
            }
         }
      }

      public void StopMonitor() {
         if (m_scantimer != null) {
            m_scantimer.Stop();
            m_scantimer = null;
         }

         if (m_worker != null) {
            m_worker.CancelAsync();
            m_worker = null;
         }

         if (m_Watcher != null) {
            m_Watcher.EnableRaisingEvents = false;
            m_Watcher.Dispose();
            m_Watcher = null;

            System.Diagnostics.Trace.WriteLine("Stop Monitor on " + m_watcherfolder);
         }
      }

      public void StopStartMonitor() // call from owner of scanner.
      {
         if (m_Watcher != null) {
            StopMonitor();
            StartMonitor();
         }
      }

      private void ScanTick(object sender, EventArgs e) {
         Debug.Assert(Application.MessageLoop);              // ensure.. paranoia

         if (m_worker != null && !m_worker.IsBusy) {
            m_worker.RunWorkerAsync();
         }
      }

      private void ScanTickWorker(object sender, System.ComponentModel.DoWorkEventArgs e) {
         var worker = sender as System.ComponentModel.BackgroundWorker;
         var entries = new List<JournalEntry>();
         e.Result = entries;
         int netlogpos = 0;
         EDJournalReader nfi = null;

         try {
            string filename = null;

            if (m_netLogFileQueue.TryDequeue(out filename))      // if a new one queued, we swap to using it
            {
               nfi = OpenFileReader(new FileInfo(filename));
               lastnfi = nfi;
               System.Diagnostics.Trace.WriteLine(string.Format("Change in file, scan {0}", lastnfi.FileName));
            } else if (ticksNoActivity >= 30 && (lastnfi == null || (!File.Exists(lastnfi.FileName) || lastnfi.filePos >= new FileInfo(lastnfi.FileName).Length))) {
               if (lastnfi == null) {
                  Trace.Write($"No last file - scanning for journals");
               } else if (!File.Exists(lastnfi.FileName)) {
                  Trace.WriteLine($"File {lastnfi.FileName} not found - scanning for journals");
               } else {
                  Trace.WriteLine($"No activity on {lastnfi.FileName} for 60 seconds ({lastnfi.filePos} >= {new FileInfo(lastnfi.FileName).Length} - scanning for new journals");
               }

               HashSet<string> tlunames = new HashSet<string>(TravelLogUnit.GetAllNames());
               string[] filenames = Directory.EnumerateFiles(m_watcherfolder, "Journal.*.log", SearchOption.AllDirectories)
                                             .Select(s => new { name = Path.GetFileName(s), fullname = s })
                                             .Where(s => !tlunames.Contains(s.name))
                                             .OrderBy(s => s.name)
                                             .Select(s => s.fullname)
                                             .ToArray();
               ticksNoActivity = 0;
               foreach (var name in filenames) {
                  nfi = OpenFileReader(new FileInfo(name));
                  lastnfi = nfi;
                  break;
               }
            } else {
               nfi = lastnfi;
            }

            ticksNoActivity++;

            if (nfi != null) {
               if (nfi.TravelLogUnit.id == 0) {
                  nfi.TravelLogUnit.type = 3;
                  nfi.TravelLogUnit.Add();
               }

               netlogpos = nfi.TravelLogUnit.Size;
               List<JournalEntry> ents = nfi.ReadJournalLog().ToList();

               using (SQLiteConnectionUser cn = new SQLiteConnectionUser(utc: true)) {
                  using (DbTransaction txn = cn.BeginTransaction()) {
                     foreach (JournalEntry je in ents) {
                        entries.Add(je);
                        je.Add(cn, txn);
                        ticksNoActivity = 0;
                     }

                     txn.Commit();
                  }
               }

               nfi.TravelLogUnit.Update();
            }

            if (worker.CancellationPending) {
               e.Cancel = true;
            }
         } catch (Exception ex) {
            // Revert and re-read the failed entries
            if (nfi != null && nfi.TravelLogUnit != null) {
               nfi.TravelLogUnit.Size = netlogpos;
            }

            System.Diagnostics.Trace.WriteLine("Net tick exception : " + ex.Message);
            System.Diagnostics.Trace.WriteLine(ex.StackTrace);
            throw;
         }
      }

      private void ScanTickDone(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e) {
         Debug.Assert(Application.MessageLoop);              // ensure.. paranoia

         if (e.Error == null && !e.Cancelled) {
            List<JournalEntry> entries = (List<JournalEntry>)e.Result;

            foreach (var ent in entries)                    // pass them to the handler
            {
               System.Diagnostics.Trace.WriteLine(string.Format("New entry {0} {1}", ent.EventTimeUTC, ent.EventTypeStr));
               if (OnNewJournalEntry != null)
                  OnNewJournalEntry(ent);
            }
         }
      }

      private void OnNewFile(object sender, FileSystemEventArgs e)        // only picks up new files
      {                                                                   // and it can kick in before any data has had time to be written to it...
         string filename = e.FullPath;
         m_netLogFileQueue.Enqueue(filename);
      }

   }
}
