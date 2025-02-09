using AutoMapper;
using ShortUrl.Core.DTO;
using ShortUrl.Core.Models;
using ShortUrl.Entities;

namespace ShortUrl.Core.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MapDtosToEntities();
            MapEntitiesToModels();
        }

        private void MapDtosToEntities()
        {
            CreateMap<UrlDTO, UrlEntity>()
                   .ForMember(dest => dest.OriginalUrl, opt => opt.MapFrom(src => src.Url))
                   .ForMember(dest => dest.ExpiryDate, opt => opt.MapFrom(src => src.ExpiryDate ?? DateTime.UtcNow.AddDays(7)))
                   .ForMember(dest => dest.IdUser, opt => opt.MapFrom(src => src.IdUser <= 0 ? null : src.IdUser));

            CreateMap<UserRegisterDTO, UserEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
        }

        private void MapEntitiesToModels()
        {
            CreateMap<UserEntity, UserCredentialsModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));

            CreateMap<UrlEntity, UrlProfileDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.OriginalUrl, opt => opt.MapFrom(src => src.OriginalUrl))
                .ForMember(dest => dest.ShortUrl, opt => opt.MapFrom(src => src.OriginalUrl))
                .ForMember(dest => dest.Clicks, opt => opt.MapFrom(src => src.Clicks))
                .ForMember(dest => dest.LastClick, opt => opt.MapFrom(src => src.LastClick))
                .ForMember(dest => dest.ExpiryDate, opt => opt.MapFrom(src => src.ExpiryDate));

            CreateMap<UserEntity, UserProfileDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Urls, opt => opt.MapFrom(src => src.Urls));
        }
    }
}