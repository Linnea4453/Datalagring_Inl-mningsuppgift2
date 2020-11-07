using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using ClassLibrary.Data;
using ClassLibrary.Models;
using Newtonsoft.Json;
using Windows.ApplicationModel.ConversationalAgent;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Uwp
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CasePage : Page
    {
        private IEnumerable<Case> cases { get; set; }
        public CaseViewModel viewModel { get; set; }
        public CasePage()
        {
            this.InitializeComponent();
            LoadCasesAsync().GetAwaiter();
            LoadStatusAsync().GetAwaiter();
            viewModel = new CaseViewModel();
        }



        //public async Task PopulateCustomerViewModel(string fileName, string filePath)
        //{
        //    var settings = JsonConvert.DeserializeObject<ObservableCollection<Customer>>(await FileHeloer.GetFileContentAsync("settings.json"));

        //    foreach (var setting in settings)
        //    {
        //        customerViewModel.Customers.Add(status);
        //    }
        //}

        //public static async Task<IEnumerable<string>> GetStatus()
        //{
        //    var list = new List<string>();

        //    StorageFile settingsFile = await ApplicationData.Current.LocalFolder.GetFileAsync("settings.json");

        //    var settings = JsonConvert.DeserializeObject<Settings>(await FileIO.ReadTextAsync(settingsFile));

        //    //var jsonsomintefinns = "{\"status\": [\"new\", \"ongoing\", \"Closed\"]}"; // Här är det hårdkodat, vi ska hämta från en fil
        //    //var settings = JsonConvert.DeserializeObject<Settings>(jsonsomintefinns);

        //    foreach (var status in settings.status)
        //    {
        //        list.Add(status);
        //    }
        //    return list;
        //}


        private async Task LoadStatusAsync()
        {
            cmbStatus.ItemsSource = await SettingsContext.GetStatus();
        }

        private async Task LoadCasesAsync() //Hämtar ärendena
        {

            cmbCasesByTitle.ItemsSource = await SqliteContext.GetCases();
            cases = await SqliteContext.GetCases();
            LoadActiveCases();
            LoadClosedCases();
        }


        private void LoadActiveCases()       //Lägger Aktivaärendena i lista 
        {
            lvActiveCases.ItemsSource = cases
                .Where(i => i.Status != "closed")
                .OrderByDescending(i => i.Created)
                .Take(SettingsContext.GetMaxItemsCount())
                .ToList();

        }

        private void LoadClosedCases()       //Lägger Stängdaärendena i lista 
        {
            lvClosedCases.ItemsSource = cases.Where(i => i.Status == "closed").ToList();
        }
    }
   
}
