using AutoMapper;
using Entities.Models;
using Entities.DataTransferObjects;

namespace CompanyEmployees.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDto>()
                .ForMember(c => c.FullAdress, opt => opt.MapFrom(m => string.Join(' ', m.Address, m.Country)));
            CreateMap<Employee, EmployeeDto>();
            CreateMap<CompanyForCreateDto, Company>();
            CreateMap<CompanyForUpdateDto, Company>();
            CreateMap<EmployeeForCreateDto, Employee>();
            CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap();
        }
    }
}
