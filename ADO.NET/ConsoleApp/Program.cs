using System;
using System.Data.SqlClient;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = @"Server=.;Database=SandboxDb;Integrated Security=true;";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("o");
            }
        }
    }
}
