using Company.service.DTOs;

namespace Company.service.Interfaces
{
    public interface IEmployeeService
    {
        EmployeeDto GetById(int? id);

        EmployeeDto GetByIdWithNoTracking(int? id);

        IEnumerable<EmployeeDto> GetAll();
        void Add(EmployeeDto employee);
        void Update(EmployeeDto employee);
        void Delete(EmployeeDto employee);
        IEnumerable<EmployeeDto> GetEmployeeByName(string name);
    }
}
