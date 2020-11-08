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
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using static ClassLibrary.Data.SettingsContext;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Uwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        private SettingsViewModel personViewModel { get; set; }
        public SettingsPage()
        {
            this.InitializeComponent();
            personViewModel = new SettingsViewModel();
        }

        public async Task PopulateCustomerViewModel(string fileName, string filePath)
        {
                var settings = JsonConvert.DeserializeObject<ObservableCollection<Settings>>(await SettingsContext.GetSettingsInformationFromJson());

                foreach (var setting in settings)
                {
                    personViewModel.Settings.Add(setting);
                }
        }

    }
}
