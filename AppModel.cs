using System.Collections.Generic;
using ED_Explorer.Models;
using ED_Explorer.Models.AuthenticationResponse;
using ED_Explorer.Models.DataModels;

namespace ED_Explorer {
   public static class AppModel {
      public static Settings Settings { get; set; }
      public static string DbPath { get; set; }
      public static int JwtExpiration { get; set; }
      public static string CurrentToken { get; set; }
      public static User User { get; set; }
      public static ICollection<JournalFile> Files { get; set; }
      public static ICollection<Expedition> Expeditions { get; set; }  
   }
}
