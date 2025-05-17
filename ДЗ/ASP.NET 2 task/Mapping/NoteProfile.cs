using AutoMapper;
using ConsProj33.Dto_Vm;
using ConsProj33.Models;

namespace ConsProj33.Mapping
{
    public class NoteProfile : Profile
    {
        public NoteProfile()
        {
            CreateMap<NoteAddDto, Note>()
            .ForMember(dest => dest.IsCompleted, opt => opt.MapFrom(_ => false))
            .ForMember(dest => dest.NoteCreationTime, opt => opt.MapFrom(_ => DateTime.Now));

            CreateMap<Note, NoteVM>();
            CreateMap<NoteUpdateDto, Note>();

        }
    }
}