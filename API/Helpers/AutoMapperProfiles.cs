using API.Dtos;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Destination, DestinationDto>();
            CreateMap<RegisterDestinationDto, Destination>();
            CreateMap<Photo, PhotoDto>();
            CreateMap<AboutDto, About>();
            CreateMap<RegisterAboutDto, About>();
            CreateMap<AboutUpdateDto, About>();
            CreateMap<About, AboutDto>();
            CreateMap<VivaPhoto, VivaPhotoDto>();
            CreateMap<DestinationDto, Destination>();
            CreateMap<Destination, Destination>();
            CreateMap<DestinationUpdateDto, Destination>();


        }
    }
}
