using HelloNHibernate.Entities;
using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace HelloNHibernate
{
    class Program
    {
        static ISessionFactory factory;

        static void Main(string[] args)
        {
            var tomas = new Employee();
            tomas.Name = "Tomas Ligotti";
            Console.WriteLine(tomas.SayHello());

            Console.Read();
        }

        static void CreateEmployeeAndSaveToDatabase()
        {
            var alex = new Employee();
            alex.Name = "Alexander the Great";

            using var session = OpenSession();
            using var transaction = session.BeginTransaction();

            try
            {
                session.Save(alex);
                transaction.Commit();
            }
            catch (Exception exception)
            {
                // Log the exception message.
                Console.WriteLine(exception);
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                throw;
            }

            Console.WriteLine("Saved Alexander the Great to db!");
        }

        static void LoadEmployeeFromDatabase()
        {
            using var session = OpenSession();

            // This is an example of HQL (hibernate query language) which will be translated into SQL by NHibernate (at runtime)
            var query = session.CreateQuery("From Employee as emp order by emp.name asc");

            IList<Employee> employees = query.List<Employee>();
            
            Console.WriteLine($"\n {employees.Count} employees found:");
            foreach (var employee in employees)
            {
                Console.WriteLine(employee.SayHello());
            }
        }

        /// <summary>
        /// Returns the session object that we can use to save, load, and search objects in db.
        /// This function is created only for example purpose.
        /// Please, do not use such approach in your real/production applications (more lightweight approach can be used).
        /// </summary>
        /// <returns><see cref="ISession"/> is the main runtime interface between a .NET app and NHibernate.
        /// The lifecycle of a ISession is bounded by the beggining and end of a logical transaction.
        /// long transactions might span several db transactions)
        /// Changes to persistent instances are detected at flush time.</returns>
        static ISession OpenSession()
        {
            if (factory is null)
            {
                var configuration = new Configuration();
                configuration.AddAssembly(Assembly.GetCallingAssembly());
                factory = configuration.BuildSessionFactory();
            }
            return factory.OpenSession();
        }
    }
}
