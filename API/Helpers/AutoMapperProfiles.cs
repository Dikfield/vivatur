using API.Dtos;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<VivaPhoto, VivaPhotoDto>().ReverseMap();

            CreateMap<Destination, DestinationDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src =>
                src.DestinationPhotos.FirstOrDefault(x => x.IsMain).Url)).ReverseMap();
            CreateMap<DestinationPhoto, DestinationPhotoDto>().ReverseMap();
            CreateMap<DestinationUpdateDto, Destination>().ReverseMap();
            CreateMap<Destination, RegisterDestinationDto>().ReverseMap();
            CreateMap<RegisterDestinationDto, Destination>().ReverseMap();

            CreateMap<Promotion, PromotionDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src =>
                src.PromotionPhotos.FirstOrDefault(x => x.IsMain).Url)).ReverseMap();
            CreateMap<PromotionPhotoDto, PromotionPhoto>().ReverseMap();
            CreateMap<PromotionUpdateDto, Promotion>().ReverseMap();
            CreateMap<Promotion, RegisterPromotionDto>().ReverseMap();
            CreateMap<RegisterPromotionDto, Promotion>().ReverseMap();

            CreateMap<AboutDto, About>().ReverseMap();
            CreateMap<RegisterAboutDto, About>().ReverseMap();
            CreateMap<AboutUpdateDto, About>().ReverseMap();
            CreateMap<AboutDto, About>().ReverseMap();

            CreateMap<Feedback, RegisterFeedDto>().ReverseMap();
            CreateMap<Feedback, FeedbackDto>().ReverseMap();
            CreateMap<Feedback, FeedbackUpdateDto>().ReverseMap();
            CreateMap<FeedbackPhotoDto, FeedbackPhoto>().ReverseMap();

        }
    }
}
