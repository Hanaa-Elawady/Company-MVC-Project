namespace Company.Repository.Interfaces
{
    public interface IUnitOfWork 
    {
        public IDepartmentRepository DepartmentRepository { get; set; }
        public IEmployeeRepository EmployeeRepository { get; set; }
        int Complete();
    }
}
