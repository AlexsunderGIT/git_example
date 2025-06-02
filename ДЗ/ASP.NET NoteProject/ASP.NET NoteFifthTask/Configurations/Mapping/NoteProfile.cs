using AutoMapper;
using ConsoleProject.NET.Contract;
using ConsoleProject.NET.Models;

namespace ConsoleProject.NET.Configurations.Mapping;

public class NoteProfile : Profile
{
    public NoteProfile()
    {
        CreateMap<PriorityDto, Priority>().ConvertUsing(src => (Priority)src);
        CreateMap<Priority, PriorityDto>().ConvertUsing(src => (PriorityDto)src);

        CreateMap<NoteAddDto, Note>()
            .ForMember(dest => dest.IsCompleted, opt => opt.MapFrom(_ => false))
            .ForMember(dest => dest.NoteCreationTime, opt => opt.MapFrom(_ => DateTime.Now))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.priority));

        CreateMap<NoteUpdateDto, Note>()
            .ForMember(dest => dest.Priority, opt => opt.PreCondition(src => src.priority.HasValue))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.priority!.Value))
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Note, NoteVM>()
            .ForMember(dest => dest.priority, opt => opt.MapFrom(src => (PriorityDto)src.Priority));
    }
}