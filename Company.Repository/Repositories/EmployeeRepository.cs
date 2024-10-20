using Company.Data.Context;
using Company.Data.Entities;
using Company.Repository.Interfaces;

namespace Company.Repository.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly Company2DbContext _context;

        public EmployeeRepository(Company2DbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetEmployeeByName(string name)
        => _context.Employees.Where(Employee => Employee.Name.Trim().ToLower().Contains(name.Trim().ToLower())).ToList();
        

        public IEnumerable<Employee> GetEmployeesByAddress(string address)
        {
            throw new NotImplementedException();
        }
    }
}
