using System;
using System.Data;
using System.Data.SqlClient;

namespace ConsoleApp.DataSets
{
    public sealed class DataSetExamples : IDisposable
    {
        private readonly string connectionString;
        private readonly SqlConnection sqlConnection;

        public DataSetExamples()
        {
            connectionString = @"Server=.;Database=usersdb;Integrated Security=true;";
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
        }

        public void ShowAllExamples()
        {
            var selectAllSqlExpression = 
                @"SELECT * FROM Persons";

            var dataSet = GetDataSet(selectAllSqlExpression);

            
            Console.WriteLine("DataSet examples: ");
            WriteDataSetIntoConsole(dataSet);
        }

        public void Dispose()
        {
            sqlConnection.Close();
        }

        private DataSet GetDataSet(string sqlExpression)
        {
            var adapter = new SqlDataAdapter(sqlExpression, sqlConnection);

            var dataSet = new DataSet();
            adapter.Fill(dataSet);

            return dataSet;
        }

        private void WriteDataSetIntoConsole(DataSet dataSet)
        {
            foreach (DataTable table in dataSet.Tables)
            {
                foreach (DataColumn column in table.Columns)
                {
                    Console.Write($"{column.ColumnName}\t");
                }
                Console.WriteLine();
                foreach (DataRow row in table.Rows)
                {
                    var cells = row.ItemArray;
                    foreach (object cell in cells)
                    {
                        Console.Write($"{cell}\t");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
