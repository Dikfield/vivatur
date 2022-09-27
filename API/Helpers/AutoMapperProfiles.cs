using API.Dtos;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Destination, DestinationDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src =>
                src.DestinationPhotos.FirstOrDefault(x => x.IsMain).Url));

            CreateMap<DestinationDescription, DestinationDescriptionDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.DescriptionPhoto.Url));

            CreateMap<DestinationDescriptionDto, DestinationDescription>();
            CreateMap<DestinationDto, Destination>();
            CreateMap<DestinationPhotoDto, DestinationPhoto>();
            CreateMap<RegisterDestinationDescriptionDto, DestinationDescription>();

            CreateMap<DestinationPhoto, DestinationPhotoDto>();

            CreateMap<RegisterDestinationDto, Destination>();

            CreateMap<AboutDto, About>();
            CreateMap<RegisterAboutDto, About>();
            CreateMap<AboutUpdateDto, About>();
            CreateMap<About, AboutDto>();
            CreateMap<VivaPhoto, VivaPhotoDto>();

            CreateMap<Destination, Destination>();
            CreateMap<DestinationUpdateDto, Destination>();
            CreateMap<Destination, RegisterDestinationDto>();
            CreateMap<DestinationDescriptionUpdateDto, DestinationDescription>();


            CreateMap<Promotion, PromotionDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src =>
                src.PromotionPhotos.FirstOrDefault(x => x.IsMain).Url));

            CreateMap<PromotionDescription, PromotionDescriptionDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.PromotionDescriptionPhoto.Url));

            CreateMap<PromotionDescriptionDto, PromotionDescription>();
            CreateMap<PromotionDto, Promotion>();
            CreateMap<PromotionPhotoDto, PromotionPhoto>();
            CreateMap<RegisterPromotionDescriptionDto, PromotionDescription>();

            CreateMap<PromotionPhoto, PromotionPhotoDto>();

            CreateMap<RegisterPromotionDto, Promotion>();

            CreateMap<AboutDto, About>();
            CreateMap<RegisterAboutDto, About>();
            CreateMap<AboutUpdateDto, About>();
            CreateMap<About, AboutDto>();
            CreateMap<VivaPhoto, VivaPhotoDto>();

            CreateMap<Promotion, Promotion>();
            CreateMap<PromotionUpdateDto, Promotion>();
            CreateMap<Promotion, RegisterPromotionDto>();
            CreateMap<PromotionDescriptionUpdateDto, PromotionDescription>();



        }
    }
}
