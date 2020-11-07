using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Windows.Storage;

namespace ClassLibrary.Data
{
    public static class SettingsContext
    {   
        private static Settings _settings { get; set; }

        public static async void GetSettingsInformation()
        {
            //StorageFolder storageFolder = KnownFolders.DocumentsLibrary;
            //StorageFile file = await storageFolder.GetFileAsync(fileName); Använd ngn variant på detta
            var settingsFile = "{\"status\": [\"New\", \"Active\", \"Closed\"], \"MaxItemsCount\": 5}";  // Här är det hårdkodat, vi ska hämta från en fil
            _settings = JsonConvert.DeserializeObject<Settings>(settingsFile);
        }

        //public static class JsonService
        //{
        //    public static void WriteToFile(string filename, OrderProduct orderProduct)
        //    {
        //        var json = JsonConvert.SerializeObject(orderProduct);

        //        using StreamWriter writer = new StreamWriter(filename);
        //        writer.Write(json);
        //    }
        //}

        public static async Task<IEnumerable<string>> GetStatus()
        {
            var list = new List<string>();

            foreach (var status in _settings.status)
            {
                list.Add(status);
            }

            return list;
        }


        public static int GetMaxItemsCount()
        {
            return _settings.MaxItemsCount;
        }

        public class Settings
        {
            public string[] status { get; set; }
            public int MaxItemsCount { get; set; }
        }

    }
}
