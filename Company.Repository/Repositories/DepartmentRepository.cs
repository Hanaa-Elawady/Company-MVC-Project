using Company.Data.Context;
using Company.Data.Entities;
using Company.Repository.Interfaces;

namespace Company.Repository.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(Company2DbContext context) : base(context)
        {
        }
    }
}
