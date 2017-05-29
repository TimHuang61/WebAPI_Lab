using System;
using System.Data.SqlClient;
using Dapper;

namespace Dapper_Test
{
    class Program
    {
        //換成自己的db source
        private static string connectionStr =
            "data source=HSIANG;initial catalog=Northwind;integrated security=True;MultipleActiveResultSets=True;";

        static void Main(string[] args)
        {
            Dapper_Select();
            Console.WriteLine("================");
            Dapper_Parameters();
            Console.WriteLine("================");
            Dapper_Insert();
            Console.WriteLine("================");
            Dapper_Update();
            Console.WriteLine("================");
            Dapper_Delete();

            Console.Read();
        }

        static void Dapper_Select()
        {
            using (var connection = new SqlConnection(connectionStr))
            {
                //Dapper 會自動處理連結資料庫的開關
                var customers = connection.Query<Customer>("SELECT * FROM dbo.Customers WHERE city LIKE 'Mexico%'");
                foreach (Customer customer in customers)
                {
                    Console.WriteLine(customer.CustomerID);
                }
            }
        }

        static void Dapper_Parameters()
        {
            using (var connection = new SqlConnection(connectionStr))
            {
                var customers = connection.Query<Customer>(
                    "SELECT * FROM dbo.Customers WHERE city LIKE @City or Country=@Country", new
                    {
                        City = "Mexico%",
                        Country = "UK"
                    });

                foreach (Customer customer in customers)
                {
                    Console.WriteLine(customer.CustomerID);
                }
            }
        }

        static void Dapper_Insert()
        {
            using (var connection = new SqlConnection(connectionStr))
            {
                //匿名、具名型別都可以，只要符合欄位名稱即可
                var result = connection.Execute("INSERT INTO Customers (CustomerID ,CompanyName) VALUES (@CustomerID, @CompanyName)",
                    new 
                    {
                        CustomerID = "Tim",
                        CompanyName = "StackExchange"
                    });

                Console.WriteLine("Insert Changed: {0}", result);
            }
        }

        static void Dapper_Update()
        {
            using (var cn = new SqlConnection(connectionStr))
            {
                var result = cn.Execute("UPDATE Customers SET CompanyName = 'StackOverflow' WHERE CustomerID = @CustomerID", new
                {
                    CustomerID = "Tim"
                });
                Console.WriteLine("Update Changed: {0}", result);
            }
            
        }

        static void Dapper_Delete()
        {
            using (var cn = new SqlConnection(connectionStr))
            {
                var result =  cn.Execute("DELETE FROM Customers WHERE CustomerID = @CustomerID", new
                {
                    CustomerID = "Tim"
                });
                Console.WriteLine("Delete Changed: {0}", result);
            }          
        }

        public class Customer
        {
            public string CustomerID { get; set; }

            public string CompanyName { get; set; }

            public string Address { get; set; }

            public string City { get; set; }

            public string Phone { get; set; }
        }
    }

}