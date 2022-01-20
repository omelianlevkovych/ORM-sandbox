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
            var insertSqlExpression =
                @"INSERT INTO Persons (FirstName, LastName, Age)
                  VALUES ('Omelian', 'Levkovych', 25)";
            var insertedRows = ExecuteNonQeury(insertSqlExpression);
            Console.WriteLine($"Inserted rows: {insertedRows}");

            var updateSqlExpression =
                @"UPDATE Persons
                  SET Age=20 WHERE FirstName='Omelian'";
            var updatedRows = ExecuteNonQeury(updateSqlExpression);
            Console.WriteLine($"Updated rows: {updatedRows}");

            var deleteSqlExpression =
                @"DELETE FROM Persons
                  WHERE FirstName='Omelian'";
            var deletedRows = ExecuteNonQeury(deleteSqlExpression);
            Console.WriteLine($"Deleted rows: {deletedRows}");
        }

        private int ExecuteNonQeury(string sqlExpression)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            var command = new SqlCommand(sqlExpression, connection);
            int updatedRows = command.ExecuteNonQuery();
            return updatedRows;
        }
    }
}
