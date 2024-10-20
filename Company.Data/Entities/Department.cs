namespace Company.Data.Entities
{
    public class Department :BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
