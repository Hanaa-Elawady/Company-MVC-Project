using Company.Data.Context;
using Company.Repository.Interfaces;

namespace Company.Repository.Repositories
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly Company2DbContext _context;

        public UnitOfWork(Company2DbContext context)
        {
            _context = context;
            DepartmentRepository = new DepartmentRepository(context);
            EmployeeRepository = new EmployeeRepository(context);
        }

        public IDepartmentRepository DepartmentRepository { get; set; }
        public IEmployeeRepository EmployeeRepository { get; set; }

        public int Complete()
        => _context.SaveChanges();
    }
}
