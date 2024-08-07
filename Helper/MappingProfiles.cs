﻿using AutoMapper;
using TestingHTTPie.Dto;
using TestingHTTPie.Models;
namespace TestingHTTPie.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Hobby, HobbyDto>()
                .ForMember(dest => dest.Persons, 
                    opt => opt.MapFrom(src => src.HobbyPersons.Select(hp => hp.Person)))
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.RowVersion, opt => opt.Ignore());

            CreateMap<Person, PersonDto>()
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.RowVersion, opt => opt.Ignore());

        }
    }
}
