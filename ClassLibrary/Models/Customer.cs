using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Windows.Networking.NetworkOperators;

namespace ClassLibrary.Models
{
    public class Customer
    {
        public Customer()
        {
                
        }
        public Customer(long id, string firstName, string lastName, string email, DateTime created)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Created = created;
        }

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; } 
        public DateTime Created { get; set; }

        public string DisplayName => $"{FirstName} {LastName}";

    }

    public class CustomerViewModel
    {
        public ObservableCollection<Customer> Customers { get; set; }
        public CustomerViewModel()
        {
            Customers = new ObservableCollection<Customer>();


        }
    }
}
