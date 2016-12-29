using System;

namespace ED_Explorer.Models.DataModels {
   class Expeditions {
      public int user { get; set; }
      public int id { get; set; }
      public string createdAt { get; set; }
      public string updatedAt { get; set; }
      public decimal? baseCredits { get; set; }
      public decimal? bonusCredits { get; set; }
      public decimal? distance { get; set; }
      public DateTime? beginDate { get; set; }
      public DateTime? endDate { get; set; }
      public string title { get; set; }
      public decimal? totalCredits { get; set; }
   }
}