using System;
using API.DTOs;
using API.Extensions;
using API.Models;
using AutoMapper;

namespace API.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<AppUser, MemberDto>()
            .ForMember(
                d => d.Age, 
                o => o.MapFrom(
                    s => s.DateOfBirth.CalculateAge()))//use extention method here is better performance
            .ForMember(
                d => d.PhotoUrl, //d for destination
                o => o.MapFrom( //o for origin
                    s => s.Photos.FirstOrDefault(x => x.IsMain)!.Url));//allow null
        CreateMap<Photo, PhotoDto>();
    }
}
