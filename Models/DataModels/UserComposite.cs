using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ED_Explorer.Models.DataModels {

   public class Expedition {
      public int user { get; set; }
      public int id { get; set; }
      public string createdAt { get; set; }
      public string updatedAt { get; set; }
      public object baseCredits { get; set; }
      public object bonusCredits { get; set; }
      public object distance { get; set; }
      public object beginDate { get; set; }
      public object endDate { get; set; }
      public string title { get; set; }
      public object totalCredits { get; set; }
   }

   public class JournalFile {
      public int user { get; set; }
      public int id { get; set; }
      public string createdAt { get; set; }
      public string updatedAt { get; set; }
      public string fileName { get; set; }
   }

   public class UserComposite {
      public List<Expedition> expeditions { get; set; }
      public List<JournalFile> journalFiles { get; set; }
      public string email { get; set; }
      public string commander { get; set; }
      public int id { get; set; }
      public string createdAt { get; set; }
      public string updatedAt { get; set; }
   }
}
