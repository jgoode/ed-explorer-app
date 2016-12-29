using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ED_Explorer.Models.AuthenticationResponse {

   public class User {
      public string email { get; set; }
      public string commander { get; set; }
      public int id { get; set; }
      public string createdAt { get; set; }
      public string updatedAt { get; set; }
   }

   public class Data {
      public User user { get; set; }
      public string token { get; set; }
   }

   public class Response {
      public string message { get; set; }
      public Data data { get; set; }
   }

   public class AuthenticationResponse {
      public Response response { get; set; }
   }

}
