using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using ClassLibrary.Data;
using ClassLibrary.Models;
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
       
        public CasePage()
        {
            this.InitializeComponent();
            LoadCasesAsync().GetAwaiter();
        }

        private async Task LoadCasesAsync() //Hämtar ärendena
        {
            cmbCasesByTitle.ItemsSource = await SqliteContext.GetCases();
            LoadActiveCases();
            LoadClosedCases();
        }


        private void LoadActiveCases()       //Lägger Aktivaärendena i lista 
        {
            lvActiveCases.ItemsSource = cases
                .Where(i => i.Status != "closed")
                //.OrderByDescending(i => i.Created)
               // .Take(SettingsContext.GetMaxItemsCount)
                .ToList();
        }

        private void LoadClosedCases()       //Lägger Stängdaärendena i lista 
        {
            lvClosedCases.ItemsSource = cases.Where(i => i.Status == "closed").ToList();
        }
    }
}
