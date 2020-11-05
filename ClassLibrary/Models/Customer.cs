using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }

    public class CustomerViewModel
    { 
        public ObservableCollection<Customer> People { get; set; }
        public CustomerViewModel()
        {
            People = new ObservableCollection<Customer>();
          
        }
    
    }
}
