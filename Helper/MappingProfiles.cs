using AutoMapper;
using TestingHTTPie.Dto;
using TestingHTTPie.Models;
namespace TestingHTTPie.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Hobby, HobbyDtoBase>();
            CreateMap<Hobby, HobbyDto>()
                .ForMember(dest => dest.Persons, 
                    opt => opt.MapFrom(src => src.HobbyPersons.Select(hp => hp.Person)))
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.RowVersion, opt => opt.Ignore());

            CreateMap<Person, PersonDtoBase>();
            CreateMap<Person, PersonDto>()
                .ForMember(dest => dest.Hobbies,
                    opt => opt.MapFrom(src => src.HobbyPersons.Select(hp => hp.Hobby)))
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.RowVersion, opt => opt.Ignore());

            CreateMap<HobbyPerson, HobbyPersonDto>()
                .ForMember(dest => dest.Hobby, opt => opt.MapFrom(src => src.Hobby))
                .ForMember(dest => dest.Person, opt => opt.MapFrom(src => src.Person));




        }
    }
}
