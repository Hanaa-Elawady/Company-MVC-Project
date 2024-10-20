
using Company.service.DTOs;

namespace Company.service.Interfaces
{
    public interface IDepartmentService 
    {
        DepartmentDto GetById(int? id);
        DepartmentDto GetByIdWithNoTracking(int? id);
        IEnumerable<DepartmentDto> GetAll();
        void Add(DepartmentDto entity);
        void Update(DepartmentDto entity);
        void Delete(DepartmentDto entity);
    }
}
