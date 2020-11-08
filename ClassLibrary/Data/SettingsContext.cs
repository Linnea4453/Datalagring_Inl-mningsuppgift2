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

        public static async Task<string> GetSettingsInformationFromJson()
        {
            var settingsFile = "{\"status\": [\"New\", \"Active\", \"Closed\"], \"MaxItemsCount\": 5}";  // Här är det hårdkodat, vi ska hämta från en fil
            _settings = JsonConvert.DeserializeObject<Settings>(settingsFile);

            StorageFolder storageFolder = KnownFolders.DocumentsLibrary;
            StorageFile file = await storageFolder.GetFileAsync("settings.json");

            return await FileIO.ReadTextAsync(file);
        }

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
