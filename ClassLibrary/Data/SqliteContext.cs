using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Models;
using Microsoft.Data.Sqlite;
using Windows.Networking.NetworkOperators;
using Windows.Storage;

namespace ClassLibrary.Data
{
    public static class SqliteContext
    {
        #region Configuration and Properties
        //Funktion för att skapa hela strukturen
        public static string _dbPath { get; set; }
        public static async void UseSQLite(string databaseName = "sqlite.db")
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync(databaseName, CreationCollisionOption.OpenIfExists);
            _dbPath = $"Filename = {Path.Combine(ApplicationData.Current.LocalFolder.Path, databaseName)}";

            using (var db = new SqliteConnection(_dbPath)) // skapar databasen
            {
                db.Open();
                var query = "CREATE TABLE IF NOT EXISTS Customer (Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, FirstName TEXT NOT NULL, LastName TEXT NOT NULL, Email TEXT NOT NULL, Created DATETIME NOT NULL); CREATE TABLE IF NOT EXISTS Cases (Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, CustomerId TEXT NOT NULL, Title TEXT NOT NULL, Description TEXT NOT NULL, Status TEXT NOT NULL, Created DATETIME NOT NULL, FOREIGN KEY (CustomerId) REFERENCES Customer(Id)); CREATE TABLE IF NOT EXISTS Comments (Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, CaseId TEXT NOT NULL, Description TEXT NOT NULL, Created DATETIME NOT NULL, FOREIGN KEY (CaseId) REFERENCES Cases(Id));";
                var cmd = new SqliteCommand(query, db);

                await cmd.ExecuteNonQueryAsync();
                db.Close();
            }
        }
        #endregion

        #region Create Methods

        public static async Task<long> CreateCustomerAsync(Customer customer)
        {
            long id = 0;

            using (var db = new SqliteConnection(_dbPath))
            {
                db.Open();
                var query = "INSERT INTO Customer VALUES(null,@FirstName,@LastName,@Email,@Created);";
                var cmd = new SqliteCommand(query, db);

                cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@Created", DateTime.Now);

                await cmd.ExecuteNonQueryAsync(); //Utan svar tillbaka
               // await cmd.ExecuteReaderAsync();
                cmd.CommandText = "SELECT last_insert_rowid()";
                id = (long)await cmd.ExecuteScalarAsync();        //Får tillbaka ett värde

                db.Close();
            }
            return id;
        }
        public static async Task<long> CreateCaseAsync(Case @case)
        {
            long id = 0;
            using (var db = new SqliteConnection(_dbPath))
            {
                db.Open();
                var query = "INSERT INTO Cases VALUES(null,@CustomerId,@Title,@Description,@Status,@Created);";
                var cmd = new SqliteCommand(query, db);
                cmd.Parameters.AddWithValue("@CustomerId", @case.CustomerId);
                cmd.Parameters.AddWithValue("@Title", @case.Title);
                cmd.Parameters.AddWithValue("@Description", @case.Description);
                cmd.Parameters.AddWithValue("@Status", "New");
                cmd.Parameters.AddWithValue("@Created", DateTime.Now);

                await cmd.ExecuteNonQueryAsync();

                cmd.CommandText = "SELECT last_insert_rowid()";
                id = (long)await cmd.ExecuteScalarAsync();        //Får tillbaka ett värde

                db.Close();
            }
            return id;
        }
        public static async Task CreateCommentAsync(Comment comment)
        {
 
            using (var db = new SqliteConnection(_dbPath))
            {
                db.Open();
                var query = "INSERT INTO Comments VALUES (@CaseId,@Description,@Created);";
                var cmd = new SqliteCommand(query, db);
                cmd.Parameters.AddWithValue(@"CaseId", comment.CaseId);
                cmd.Parameters.AddWithValue(@"Description", comment.Description);
                cmd.Parameters.AddWithValue(@"Created", DateTime.Now);
                await cmd.ExecuteNonQueryAsync();

                db.Close();
            }
        
        }
        #endregion

        #region Get Methods

        public static async Task<IEnumerable<Customer>> GetCustomers()
        {
            var customer = new List<Customer>();

            using (var db = new SqliteConnection(_dbPath))
            {
                db.Open();

                var query = "SELECT * FROM Customer";
                var cmd = new SqliteCommand(query, db);

                var result = await cmd.ExecuteReaderAsync();

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        customer.Add(new Customer(result.GetInt64(0), result.GetString(1), result.GetString(2),result.GetString(3), result.GetDateTime(4)));
                    }
                }
                db.Close();
            }
            return customer;
        } //Lista alla kunder i en lista

        public static async Task<IEnumerable<string>> GetCustomerNames() // Hämta  Namn
        {
            var customernames = new List<string>();

            using (var db = new SqliteConnection(_dbPath))
            {
                db.Open();

                var query = "SELECT FirstName, LastName FROM Customer";
                var cmd = new SqliteCommand(query, db);

                var result = await cmd.ExecuteReaderAsync();

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        customernames.Add(result.GetString(0));
                    }
                }

                db.Close();
            }

            return customernames;
        }

        public static async Task<Customer> GetCustomerById(long id) // Hämta med ID
        {
            var customer = new Customer();

            using (var db = new SqliteConnection(_dbPath))
            {
                db.Open();

                var query = "SELECT * FROM Customer WHERE Id = @Id";
                var cmd = new SqliteCommand(query, db);

                cmd.Parameters.AddWithValue("@Id", id);
                var result = await cmd.ExecuteReaderAsync();

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        customer = new Customer(result.GetInt64(0), result.GetString(1), result.GetString(2), result.GetString(3), result.GetDateTime(4));
                    }
                }
                db.Close();
             
            }
            return customer;
        }

        public static async Task<long> GetCustomerIdByName(string name)
        {
            long customerid = 0;
                using (var db = new SqliteConnection(_dbPath))
                    {
                         db.Open();

                         var query = "SELECT Id FROM Customer WHERE FirstName = @FirstName";
                         var cmd = new SqliteCommand(query, db);

                         cmd.Parameters.AddWithValue("@FirstName", name);
                         customerid = (long)await cmd.ExecuteScalarAsync();
                         db.Close();
                      }

            return customerid;
        }


        public static async Task<ICollection<Comment>> GetCommentsByCaseId(long caseid) 
        {
            var comments = new List<Comment>();

            using (var db = new SqliteConnection(_dbPath))
            {
                db.Open();

                var query = "SELECT * FROM Comments WHERE CaseId = @CaseId";
                var cmd = new SqliteCommand(query, db);

                cmd.Parameters.AddWithValue("CaseId", caseid);
                var result = await cmd.ExecuteReaderAsync();

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        comments.Add(new Comment(result.GetInt32(0), result.GetInt32(1), result.GetString(2), result.GetDateTime(3)));
                    }
                }
                db.Close();
                return comments;
            }
        } //Alla kommentarer i min lista

        public static async Task<IEnumerable<Case>> GetCases()
        {
            var cases = new List<Case>();

            using (var db = new SqliteConnection(_dbPath))
            {
                db.Open();

                var query = "SELECT * FROM Cases";
                var cmd = new SqliteCommand(query, db);

                var result = await cmd.ExecuteReaderAsync();

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        var @case = new Case(result.GetInt64(0), result.GetInt64(1), result.GetString(2), result.GetString(3), result.GetString(4), result.GetDateTime(5));
                        @case.Customer = await GetCustomerById(result.GetInt64(1));
                        @case.Comments = await GetCommentsByCaseId(result.GetInt64(0));
                        cases.Add(@case);
                    }
                }
                db.Close();
            }

            return cases;
        }

        #endregion

        #region Update Methods

        #endregion   SKAPA!!!!
    }
}
