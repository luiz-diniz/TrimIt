using AutoMapper;
using ShortUrl.Core.DTO;
using ShortUrl.Entities;

namespace ShortUrl.Core.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UrlDTO, Url>()
                   .ForMember(dest => dest.OriginalUrl, opt => opt.MapFrom(src => src.Url))
                   .ForMember(dest => dest.ExpiryDate, opt => opt.MapFrom(src => src.ExpiryDate ?? DateTime.UtcNow.AddDays(7)));

            CreateMap<UserRegisterDTO, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
        }
    }
}