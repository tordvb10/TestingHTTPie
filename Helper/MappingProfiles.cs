using AutoMapper;
using TestingHTTPie.Dto;
using TestingHTTPie.Models;
namespace TestingHTTPie.Helper
{

    public static class MapingExtensions
    {
        public static IMappingExpression<Tsource,TDestination> IgnoreCommenPropterties<Tsource, TDestination>(
            this IMappingExpression<Tsource, TDestination> mapping)
            where TDestination : ICommonProps
        {
            return mapping
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.RowVersion, opt => opt.Ignore());

        }
    }
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Hobby, HobbyDtoBase>()
                .ReverseMap()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.RowVersion, opt => opt.Ignore());

            CreateMap<Hobby, HobbyDto>()
                .ForMember(dest => dest.Persons, 
                    opt => opt.MapFrom(src => src.HobbyPersons.Select(hp => hp.Person)))
                .ReverseMap()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.RowVersion, opt => opt.Ignore());

            CreateMap<Person, PersonDtoBase>()
                .ReverseMap()
                .IgnoreCommenPropterties();

            CreateMap<Person, PersonDto>()
                .ForMember(dest => dest.Hobbies,
                    opt => opt.MapFrom(src => src.HobbyPersons.Select(hp => hp.Hobby)))
                .ReverseMap()
                .IgnoreCommenPropterties();

            CreateMap<HobbyPerson, HobbyPersonDto>()
                .ForMember(dest => dest.Hobby, opt => opt.MapFrom(src => src.Hobby))
                .ForMember(dest => dest.Person, opt => opt.MapFrom(src => src.Person));




        }
    }
}
