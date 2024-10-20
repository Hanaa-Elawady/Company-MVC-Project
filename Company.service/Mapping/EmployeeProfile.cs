using AutoMapper;
using Company.Data.Entities;
using Company.service.DTOs;

namespace Company.service.Mapping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
        }
    }
}
