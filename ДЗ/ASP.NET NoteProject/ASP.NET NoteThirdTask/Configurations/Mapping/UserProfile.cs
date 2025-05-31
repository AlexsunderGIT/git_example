using AutoMapper;
using ConsoleProject.NET.Contract;
using ConsoleProject.NET.Models;

namespace ConsoleProject.NET.Repositories;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserVm>();

        CreateMap<UserAddDto, User>()
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
    }
}