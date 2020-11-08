using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.UserDataAccounts;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ClassLibrary.Data;
using ClassLibrary.Models;
using Windows.ApplicationModel.ConversationalAgent;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Uwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

       
        public MainPage()
        {
            this.InitializeComponent();
           
        }

     

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(CasePage));      //Här gör det att CasePage är förstasidan
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected) //Sätt in JsonSettings här, Tex hur många jatg vill hämta. olika aktegorier

            {
                ContentFrame.Navigate(typeof(SettingsPage));
            }
            else
            {
                NavigationViewItem item = args.SelectedItem as NavigationViewItem;

                switch (item.Tag.ToString()) // Med Tag navigerar du mellan listorna
                {
                    case "CasePage":
                        ContentFrame.Navigate(typeof(CasePage));
                        break;
                    case "CustomerPage":
                        ContentFrame.Navigate(typeof(CustomerPage));
                        break;

                }
            }

           
        }
    }
}
