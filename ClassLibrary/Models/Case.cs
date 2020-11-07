using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Data;
using Microsoft.Data.Sqlite;
using Windows.ApplicationModel.UserDataAccounts;
using Windows.Storage;

namespace ClassLibrary.Models
{
    public class Case
    {
        public Case()
        {

        }
        public Case(long id, long customerId, string title, string description, string status, DateTime created)
        {
            Id = id;
            CustomerId = customerId;
            Title = title;
            Description = description;
            Status = status;
            Created = created;
        }

        public long Id { get; set; }
        public long CustomerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }

        public string Summary => $"{Id}, {CustomerId}, {Description},{Status},{Created:D}";

        public virtual Customer Customer { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

    }

    public class CaseViewModel
    { 

        public ObservableCollection<Case> CaseAllInfo { get; set; }


        public CaseViewModel()
        {
            //CaseAllInfo = new ObservableCollection<Case>(SqliteContext.GetCases());
            var list = new List<string>();


            //    using (var db = new SqliteConnection($"Filename={_dbpath}"))
            //    {
            //        db.Open();

            //        var query = "SELECT Name FROM Persons";
            //        var cmd = new SqliteCommand(query, db);

            //        var result = cmd.ExecuteReader();


            //        while (result.Read())
            //        {
            //            list.Add(result.GetString(0));
            //        }

            //        db.Close();

            //        return list;
            //    }




        }
    }
}
