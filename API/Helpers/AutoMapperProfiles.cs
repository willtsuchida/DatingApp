using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API;

public class AutoMapperProfiles : Profile
{
    //From..to
    //Auto mapper will check the prop name in App user then in Member Dto (matches)
    //GetAge() in AppUser will be "auto" mapped to Age in Member Dto (identifies "Get")
    public AutoMapperProfiles()
    {
        CreateMap<AppUser, MemberDto>()
                .ForMember(dest => dest.PhotoUrl,
                     opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculageAge())); //Populando campo PhotoUrl com a
        CreateMap<Photo, PhotoDto>();
        CreateMap<MemberUpdateDto, AppUser>();
        CreateMap<RegisterDto, AppUser>();
    }
}
