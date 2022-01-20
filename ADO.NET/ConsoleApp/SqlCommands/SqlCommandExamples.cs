using System;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.SqlCommands
{
    public class SqlCommandExamples : IDisposable
    {
        private readonly string connectionString;
        private SqlConnection sqlConnection;

        public SqlCommandExamples()
        {
            connectionString = @"Server=.;Database=usersdb;Integrated Security=true;";
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (sqlConnection != null)
                {
                    sqlConnection.Dispose();
                    sqlConnection = null;
                }
            }
        }

        public async Task ShowAllExamples()
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
            var selectAllResult = await ExecuteReader(selectAllSqlExpression);
            Console.WriteLine(selectAllResult);

            // Scalar functions
            var scalarSqlExpression =
                @"SELECT COUNT(*) FROM Persons";
            var scalarResult = await ExecuteScalar(scalarSqlExpression);
            Console.WriteLine($"Rows count: {scalarResult}");
        }

        private int ExecuteNonQeury(string sqlExpression)
        {
            var command = new SqlCommand(sqlExpression, sqlConnection);
            int updatedRows = command.ExecuteNonQuery();
            return updatedRows;
        }

        private async Task<string> ExecuteReader(string sqlExpression)
        {
            var command = new SqlCommand(sqlExpression, sqlConnection);
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
            while (await reader.ReadAsync())
            {
                int id = reader.GetInt32(0);
                string firstName = reader.GetString(1);
                string lastName = reader.GetString(2);
                int age = reader.GetInt32(3);

                result.Append($"\n {id} \t{firstName} \t{lastName} \t{age}");
            }
            return result.ToString();
        }

        private Task<object> ExecuteScalar(string sqlExpression)
        {
            var command = new SqlCommand(sqlExpression, sqlConnection);
            return command.ExecuteScalarAsync();
        }
    }
}
