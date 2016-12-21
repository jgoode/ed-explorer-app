using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ED_Explorer.Models;

namespace ED_Explorer {
   public static class AppModel {
      public static Settings Settings { get; set; }
      public static string DbPath { get; set; }

   }
}
