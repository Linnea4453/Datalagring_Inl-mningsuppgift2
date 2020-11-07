using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace ClassLibrary.Data
{
    public static class FileHelper  //hämta  fil fråm local storage Documents
    {
        public static async Task<string> GetFileContentAsync(string filename)
        {
            StorageFolder storageFolder = KnownFolders.DocumentsLibrary;
            StorageFile file = await storageFolder.GetFileAsync(filename);

            return await FileIO.ReadTextAsync(file);
        }
    }
}
