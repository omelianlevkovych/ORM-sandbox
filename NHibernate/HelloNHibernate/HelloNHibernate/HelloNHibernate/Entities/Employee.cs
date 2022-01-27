namespace HelloNHibernate.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Employee Manager { get; set; }

        public string SayHello()
        {
            return $"'Hello world!', said {Name}";
        }
    }
}
