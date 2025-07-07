using AutoMapper;
using EmpDepRoleFulstackProjectJun13.Models.Domain;
using EmpDepRoleFulstackProjectJun13.Models.DTO;

namespace EmpDepRoleFulstackProjectJun13.Mapper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Employee, GetEmployeeDTO>()
                 .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
                 .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName));
            CreateMap<Employee, UpdateEmployeeDTO>();
            CreateMap<CreateEmployeeDTO,Employee>();
        }
    }
}
