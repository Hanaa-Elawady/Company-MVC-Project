using AutoMapper;
using Company.Data.Entities;
using Company.service.DTOs;

namespace Company.service.Mapping
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentDto>().ReverseMap();

        }
    }
}
