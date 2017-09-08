using Data.Interface;

namespace Data
{
    public class Employee : IPerson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Manager Manager { get; set; }
    }
}
