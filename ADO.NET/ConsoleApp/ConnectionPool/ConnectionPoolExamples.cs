using System;
using System.Data.SqlClient;

namespace ConsoleApp.ConnectionPool
{
    public class ConnectionPoolExamples
    {
        private readonly string firstConnectionString;
        private readonly string secondConnectionString;

        public ConnectionPoolExamples()
        {
            firstConnectionString = @"Server=.;Database=usersdb;Integrated Security=true;";
            secondConnectionString = @"Server=.;Database=SandboxDb;Integrated Security=true;";
        }

        public void ShowAllExamples()
        {

            SameConnectionBecauseOfConnectionPool();
            OwnConnectionPoolPerEachConnectionConfig();
        }

        private void SameConnectionBecauseOfConnectionPool()
        {

            using var connection = new SqlConnection(firstConnectionString);
            connection.Open();
            var firstConnectionClientId = connection.ClientConnectionId;
            connection.Close();

            connection.Open();
            var secondConnectionClientId = connection.ClientConnectionId;
            connection.Close();

            Console.WriteLine($"Connection pool exists: {firstConnectionClientId == secondConnectionClientId}");
        }


        private void OwnConnectionPoolPerEachConnectionConfig()
        {
            Console.WriteLine("\n\n Connection pool is created per each connection string underneath.");

            using var firstConnection = new SqlConnection(firstConnectionString);
            firstConnection.Open();
            Console.WriteLine($"First connection string client id : {firstConnection.ClientConnectionId}");
            firstConnection.Close();

            using var secondConnection = new SqlConnection(firstConnectionString);
            secondConnection.Open();
            Console.WriteLine($"First connection string client id : {secondConnection.ClientConnectionId}");
            secondConnection.Close();

            using var thirdConnection = new SqlConnection(secondConnectionString);
            thirdConnection.Open();
            Console.WriteLine($"Second connection string client id : {thirdConnection.ClientConnectionId}");
            thirdConnection.Close();
        }
    }
}
