using AutoMapper;
using Company.Data.Entities;
using Company.Repository.Interfaces;
using Company.service.DTOs;
using Company.service.Interfaces;

namespace Company.service.Services
{
    public class DepartmentService : IDepartmentService
    {
		#region Dependancy injection
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public DepartmentService( IUnitOfWork unitOfWork , IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		#endregion

        #region  Add 
		public void Add(DepartmentDto entity)
        {
            Department department = _mapper.Map<Department>(entity);
            _unitOfWork.DepartmentRepository.Add(department);
            _unitOfWork.Complete();

        }

        #endregion

        #region Delete 
        public void Delete(DepartmentDto entity)
        {
            Department department = _mapper.Map<Department>(entity);
            _unitOfWork.DepartmentRepository.Delete(department);
            _unitOfWork.Complete();
        }


        #endregion

        #region getall
        public IEnumerable<DepartmentDto> GetAll()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
			IEnumerable<DepartmentDto> mappedDepartment = _mapper.Map<IEnumerable<DepartmentDto>>(departments);
            return mappedDepartment;
        }
        #endregion

        #region getById

        public DepartmentDto GetById(int? id)
        {
            if (id is null)
                return null;

            var department = _unitOfWork.DepartmentRepository.GetById(id.Value);

            if (department == null) 
            return null;

            
            DepartmentDto departmentDto = _mapper.Map<DepartmentDto>(department);
            return departmentDto;
            
        }
        public DepartmentDto GetByIdWithNoTracking(int? id)
        {
            if (id is null)
                return null;

            var department = _unitOfWork.DepartmentRepository.GetByIdWithNoTracking(id.Value);

            if (department == null) 
            return null;

            
            DepartmentDto departmentDto = _mapper.Map<DepartmentDto>(department);
            return departmentDto;
            
        }

        #endregion

        #region Update 
        public void Update(DepartmentDto entity)
        {
            Department department = _mapper.Map<Department>(entity);
            _unitOfWork.DepartmentRepository.Update(department);
            _unitOfWork.Complete();
        }
        #endregion
    }
}
