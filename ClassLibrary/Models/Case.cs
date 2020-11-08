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
using Newtonsoft.Json;
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

        [JsonProperty(propertyName: "id")]
        public long Id { get; set; }

        [JsonProperty(propertyName: "customerid")]
        public long CustomerId { get; set; }

        [JsonProperty(propertyName: "title")]
        public string Title { get; set; }

        [JsonProperty(propertyName: "description")]
        public string Description { get; set; }

        [JsonProperty(propertyName: "status")]
        public string Status { get; set; }

        [JsonProperty(propertyName: "created")]
        public DateTime Created { get; set; }

        public string Summary => $"{Id}, {CustomerId}, {Description},{Status},{Created:D}";

        public virtual Customer Customer { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

    }

    public class CaseViewModel
    { 

        public ObservableCollection<Case> CaseAllInfo { get; set; }
       public static string _dbPath { get; set; }

        public CaseViewModel()
        {

        var cases = new List<Case>();

            using (var db = new SqliteConnection(_dbPath))
            {
                db.Open();

                var query = "SELECT * FROM Cases";
                var cmd = new SqliteCommand(query, db);

                var result = cmd.ExecuteReader();

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        var @case = new Case(result.GetInt64(0), result.GetInt64(1), result.GetString(2), result.GetString(3), result.GetString(4), result.GetDateTime(5));
                        //@case.Add = GetCustomerById(result.GetInt64(1));
                        //@case.Comments = GetCommentsByCaseId(result.GetInt64(0));
                        cases.Add(@case);
                    }
                }
                db.Close();
            }
            return;
        }
    }
}
