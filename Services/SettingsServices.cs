using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ED_Explorer.Models;
using LiteDB;

namespace ED_Explorer.Services {
   internal class SettingsServices {

      public static Settings Save(Settings settings, string dbPath)
      {
         Settings settingsSaved;
         if (settings == null) throw new ArgumentNullException(nameof(settings));
         if (dbPath == null) throw new ArgumentNullException(nameof(dbPath));

         using (var db = new LiteDatabase(dbPath)) {
            var collection = db.GetCollection<Settings>("settings");
            collection.Insert(settings);

            settingsSaved = collection.FindAll().FirstOrDefault();
         }
         return settingsSaved;
      }

      public static Settings GetSetting(string dbPath)
      {
         if (dbPath == null) throw new ArgumentNullException(nameof(dbPath));

         Settings settings = null;
         using (var db = new LiteDatabase(dbPath))
         {
            var collection = db.GetCollection<Settings>("settings");

            settings = collection.FindAll().FirstOrDefault();
         }              
         return settings;
      }
   }
}
