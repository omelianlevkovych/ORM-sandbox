using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = @"Server=.;Database=usersdb;Integrated Security=true;";

            using var connection = new SqlConnection(connectionString);

            var watch = new Stopwatch();
            watch.Start();

            connection.OpenAsync();
            PrintConnectionState(connection);

            watch.Stop();
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
        }

        public static void PrintConnectionState(IDbConnection dbConnection)
        {
            while (dbConnection.State is not ConnectionState.Open)
            {
                Console.WriteLine("Connection is closed");
            }
            Console.WriteLine("Connection is open");
        }
    }
}
