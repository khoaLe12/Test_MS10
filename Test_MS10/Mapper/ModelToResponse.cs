using AutoMapper;
using Test_MS10.Common;
using Test_MS10.Entity;
using Test_MS10.ViewModel;

namespace Test_MS10.Mapper;

public class ModelToResponse : Profile
{
    public ModelToResponse()
    {
        CreateMap<Employee, EmployeeResponseVM>()
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => (DateTime.Now.Year - src.DoB.Year)))
            .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.GetPosition()));
    }
}
