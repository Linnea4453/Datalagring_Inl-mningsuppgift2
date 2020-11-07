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
using Windows.Storage;
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
    public sealed partial class CustomerPage : Page
    {
        private IEnumerable<Case> cases { get; set; }
       
      

        public CustomerPage()
        {
            this.InitializeComponent();
           // ViewModel = new CustomerViewModel();
            LoadCustomersAsync().GetAwaiter();
            LoadCasesAsync().GetAwaiter();
        }
         
        private async Task LoadCustomersAsync()
        {
            cmbCustomers.ItemsSource = await SqliteContext.GetCustomerNames();
        }

        private async Task LoadCasesAsync()
        {
            cases = await SqliteContext.GetCases();
               //LoadActiveCases();
               //LoadClosedCses();
        }

        private async void CreateCustomer_Click(object sender, RoutedEventArgs e)
        {

            //TryCatch Get custoemr

            await SqliteContext.CreateCustomerAsync(new Customer
            {
                FirstName = TbFirstName.Text,
                LastName = TbLastName.Text,
                Email = TbEmail.Text,
            });

            await LoadCustomersAsync();
        }

        private async void CreateCases_Click(object sender, RoutedEventArgs e)
        {
            await SqliteContext.CreateCaseAsync(
                new Case
                {
                    Title = TbTitle.Text,
                    Description = TbDescription.Text,
                    CustomerId = await SqliteContext.GetCustomerIdByName(cmbCustomers.SelectedItem.ToString())
                });

            await LoadCasesAsync();
        }
    }
}
