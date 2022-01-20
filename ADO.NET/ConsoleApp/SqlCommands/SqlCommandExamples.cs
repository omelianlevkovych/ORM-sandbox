using System;
using System.Data.SqlClient;
using System.Text;

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

            // Select after Insert
            var selectAllSqlExpression =
                @"SELECT * FROM Persons";
            ExecuteNonQeury(insertSqlExpression);
            var selectAllResult = ExecuteReader(selectAllSqlExpression);
            Console.WriteLine(selectAllResult);
        }

        private int ExecuteNonQeury(string sqlExpression)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            var command = new SqlCommand(sqlExpression, connection);
            int updatedRows = command.ExecuteNonQuery();
            return updatedRows;
        }

        private string ExecuteReader(string sqlExpression)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            var command = new SqlCommand(sqlExpression, connection);
            using var reader = command.ExecuteReader();
            if (!reader.HasRows)
            {
                throw new ApplicationException("The table is empty");
            }

            var result = new StringBuilder();

            for (int i = 0; i < reader.FieldCount; ++i)
            {
                result.Append($"{reader.GetName(i)} \t");
            }
            while (reader.Read())
            {
                // TODO: feels bad, can we do this without casting?
                object id = reader.GetValue(0);
                object firstName = reader.GetValue(1);
                object lastName = reader.GetValue(2);
                object age = reader.GetValue(3);

                result.Append($"\n {id} \t{firstName} \t{lastName} \t{age}");
            }
            return result.ToString();
        }
    }
}
