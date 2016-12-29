using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ED_Explorer.Services {
   class FileService {
      private long _lastPosition;
      private string _filePath;

      public FileService(string filePath) {
         _filePath = filePath;
         _lastPosition = 0;
      }

      public ICollection<string> ReadData() {
         var data = new List<string>();
         using (var stream = File.Open(_filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
            stream.Seek(_lastPosition, SeekOrigin.Begin);
            using (var streamReader = new StreamReader(stream)) {
               string line = null;
               while ((line = streamReader.ReadLine()) != null) {
                   data.Add(line);
               }
               _lastPosition = stream.Position;
            }
         }
         return data;
      }
   }
}
