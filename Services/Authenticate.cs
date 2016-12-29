using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ED_Explorer.Models;
using ED_Explorer.Models.AuthenticationResponse;
using ED_Explorer.Models.DataModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ED_Explorer.Services {
   internal class Authenticate {
      public static void Login() {
         var url = "https://explorer-api.jhgmedia.com/login";
         var client = new HttpClient();

         var settingsJson = JsonConvert.SerializeObject(new {
            email = AppModel.Settings.Email,
            password = Encryption.Decrypt(AppModel.Settings.Password)
         });
         var response = client.PostAsync(url, new StringContent(settingsJson)).Result;

         if (response.IsSuccessStatusCode) {
            AuthenticationResponse content = JsonConvert.DeserializeObject<AuthenticationResponse>(response.Content.ReadAsStringAsync().Result);
            if (content.response.message == "Successfully signed in") {
               AppModel.CurrentToken = content.response.data.token;
               AppModel.User = content.response.data.user;
            }
         }
      }
   }

   internal class UserService {
      public static void GetUserData() {
         var url = $"https://explorer-api.jhgmedia.com/user/{AppModel.User.id}";
         var client = new HttpClient();
         client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AppModel.CurrentToken);

         var response = client.GetAsync(url).Result;
         if (response.IsSuccessStatusCode) {
            UserComposite content = JsonConvert.DeserializeObject<UserComposite>(response.Content.ReadAsStringAsync().Result);
            AppModel.Expeditions = content.expeditions;
            AppModel.Files = content.journalFiles;
         }
      }
   }
}
