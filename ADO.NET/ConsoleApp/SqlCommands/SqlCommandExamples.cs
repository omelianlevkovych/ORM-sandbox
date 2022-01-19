using System;
using System.Data.SqlClient;

namespace ConsoleApp.SqlCommands
{
    public class SqlCommandExamples
    {
        private readonly string connectionString;

        public SqlCommandExamples()
        {
            connectionString = @"Server=.;Database=usersdb;Integrated Security=true;";
        }

        public void ShowAllExamples()
        {
            Console.WriteLine($"Updated rows: {UpdatePersons()}");
        }

        private int UpdatePersons()
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            var sqlExpression =
                @"INSERT INTO Persons (FirstName, LastName, Age)
                  VALUES ('Omelian', 'Levkovych', 25)";

            var command = new SqlCommand(sqlExpression, connection);
            int updatedRows = command.ExecuteNonQuery();
            return updatedRows;
        }
    }
}
