using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ED_Explorer.Services {
   internal class Encryption {
      static readonly string PasswordHash = "B92B9D1E";
      static readonly string SaltKey = "F9325951";
      static readonly string VIKey = "@DB4f3D2z5F6g7L3";

      public static string Encrypt(string plainText) {
         byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

         byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
         var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
         var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

         byte[] cipherTextBytes;

         using (var memoryStream = new MemoryStream()) {
            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write)) {
               cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
               cryptoStream.FlushFinalBlock();
               cipherTextBytes = memoryStream.ToArray();
               cryptoStream.Close();
            }
            memoryStream.Close();
         }
         return Convert.ToBase64String(cipherTextBytes);
      }

      public static string Decrypt(string encryptedText) {
         byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
         byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
         var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

         var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
         var memoryStream = new MemoryStream(cipherTextBytes);
         var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
         byte[] plainTextBytes = new byte[cipherTextBytes.Length];

         int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
         memoryStream.Close();
         cryptoStream.Close();
         return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
      }
   }
}
