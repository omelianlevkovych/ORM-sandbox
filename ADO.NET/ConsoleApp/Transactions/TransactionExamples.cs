using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Transactions
{
    public sealed class TransactionExamples : IDisposable
    {
        private readonly string connectionString;
        private readonly SqlConnection sqlConnection;

        public TransactionExamples()
        {
            connectionString = @"Server=.;Database=usersdb;Integrated Security=true;";
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
        }

        public void Dispose()
        {
            sqlConnection.Close();
        }

        public void InsertWithTransaction()
        {
            using var transaction = sqlConnection.BeginTransaction();
            using var command = sqlConnection.CreateCommand();
            command.Transaction = transaction;

            try
            {
                command.CommandText = "INSERT INTO Persons (FirstName, LastName, Age) VALUES ('David', 'Foster', 42)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO Persons (FirstName, LastName, Age) VALUES ('Tomas', 'Ligotti', 65)";
                command.ExecuteNonQuery();

                transaction.Commit();
            }
            catch (Exception exception)
            {
                // log the exception here
                Console.WriteLine($"Exception message: {exception.Message}");
                transaction.Rollback();
            }
        }

        public void InsertWithFailingTransaction()
        {
            using var transaction = sqlConnection.BeginTransaction();
            using var command = sqlConnection.CreateCommand();
            command.Transaction = transaction;

            try
            {
                command.CommandText = "INSERT INTO Persons (FirstName, LastName, Age) VALUES ('First', 'Transaction', 1)";
                command.ExecuteNonQuery();
                
                // some error occured
                throw new ApplicationException("Some app exception has been thrown");

                command.CommandText = "INSERT INTO Persons (FirstName, LastName, Age) VALUES ('Second', 'Transaction', 2)";
                command.ExecuteNonQuery();

                transaction.Commit();
            }
            catch (Exception exception)
            {
                // log the exception here
                Console.WriteLine($"Exception message: {exception.Message}");
                transaction.Rollback();
            }
        }
    }
}
