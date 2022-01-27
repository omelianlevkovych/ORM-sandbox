using ConsoleApp.Models;
using System;
using System.Data;
using Dapper;
using System.Data.SqlClient;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = @"Server=.;Database=usersdb;Integrated Security=true;";
            using IDbConnection db = new SqlConnection(connectionString);

            var persons = db.Query<Person>("SELECT * FROM Persons").ToList();

            foreach (Person p in persons)
            {
                Console.WriteLine($"{p.Id} | {p.FirstName} | {p.LastName} | {p.Age}");
            }

            Console.Read();
        }
    }
}
