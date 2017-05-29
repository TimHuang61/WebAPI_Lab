using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper
{
    //package managet console: Install-Package Dapper
    class Program
    {
        //換成自己的db source
        private static string connectionStr = "data source=HSIANG;initial catalog=Northwind;integrated security=True;MultipleActiveResultSets=True;";

        static void Main(string[] args)
        {
            Dapper_Select();

            Console.Read();
        }

        static void Dapper_Select()
        {
            using (var connection = new SqlConnection(connectionStr))
            {
                connection.Open();
                var customers = connection.Query("SELECT * FROM dbo.Customers WHERE city LIKE 'Mexico%");
                foreach (Customer customer in customers)
                {
                    Console.WriteLine(customer.CustomerID);
                }
            }
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
