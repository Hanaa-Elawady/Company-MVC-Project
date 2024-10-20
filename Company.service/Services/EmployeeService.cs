using AutoMapper;
using Company.Data.Entities;
using Company.Repository.Interfaces;
using Company.service.DTOs;
using Company.service.Interfaces;
using Company.Services.Helper;

namespace Company.service.Services
{
	public class EmployeeService : IEmployeeService
    {
		#region dependancy injection
		    private readonly IUnitOfWork _unitOfWork;
		    private readonly IMapper _mapper;

		    public EmployeeService(IUnitOfWork unitOfWork , IMapper mapper)
		    {
			    _unitOfWork = unitOfWork;
			    _mapper = mapper;
		    }
		#endregion

		#region Add
		public void Add(EmployeeDto employee)
        {
            employee.ImageUrl = DocumentSettings.UploadFile(employee.Image ,"EmployeeImages");
            Employee mappedemployee = _mapper.Map<Employee>(employee);
            _unitOfWork.EmployeeRepository.Add(mappedemployee);
            _unitOfWork.Complete();

        }

		#endregion

		#region Delete
		public void Delete(EmployeeDto employeeDto)
        {
            Employee employee = _mapper.Map<Employee>(employeeDto);
            _unitOfWork.EmployeeRepository.Delete(employee);
            _unitOfWork.Complete();
        }
		#endregion

		#region Get all
		public IEnumerable<EmployeeDto> GetAll()
        {
            var Employees = _unitOfWork.EmployeeRepository.GetAll();
			IEnumerable<EmployeeDto> employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(Employees);
            return employeeDtos;
		}
		#endregion

		#region Get By Id 
		public EmployeeDto GetById(int? id)
        {
            if (id == null)
                return null;

            var employee = _unitOfWork.EmployeeRepository.GetById(id.Value);
            if (employee != null)
            {
                EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(employee);
                return employeeDto;
            }
                return null;
        }

        public EmployeeDto GetByIdWithNoTracking(int? id)
        {
			if (id == null)
				return null;

			var employee = _unitOfWork.EmployeeRepository.GetByIdWithNoTracking(id.Value);
			if (employee != null)
			{
				EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(employee);
				return employeeDto;
			}
			return null;
		}

		#endregion

		#region Search By Name 
		public IEnumerable<EmployeeDto> GetEmployeeByName(string name)
        {
			var employees = _unitOfWork.EmployeeRepository.GetEmployeeByName(name);
			IEnumerable<EmployeeDto> mappedEmployee = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
			return mappedEmployee;
		}

		#endregion

		#region Update
		public void Update(EmployeeDto employee)
        {
				employee.ImageUrl = DocumentSettings.UploadFile(employee.Image, "EmployeeImages");
				Employee mappedemployee = _mapper.Map<Employee>(employee);
				_unitOfWork.EmployeeRepository.Update(mappedemployee);
				_unitOfWork.Complete();
		}

		#endregion

    }
}
