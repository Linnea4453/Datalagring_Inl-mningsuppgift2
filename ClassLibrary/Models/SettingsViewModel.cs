using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ClassLibrary.Data.SettingsContext;

namespace ClassLibrary.Models
{
    public class SettingsViewModel
    {
        public ObservableCollection<Settings> Settings { get; set; }
        public SettingsViewModel()
        { 
        }
    }
}
