using API.Dtos;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IDestination
    {
        Task<bool> SaveAllAsync();
        Task<IEnumerable<DestinationDto>> GetAllAsync();
        Task<DestinationDto> GetByNameAsync(string name);
        Task<DestinationDto> GetDestinationByIdAsync(int id);
        Task<DestinationPhoto> GetPhotoByIdAsync(int id);
        Task<bool> SavePhoto(DestinationPhoto photo);
        void DeletePhoto(DestinationPhoto photo);
        public Task<bool> DeleteDestination(Destination dest);
        void Register(Destination dest);
        void RegisterDescription(DestinationDescription desc);
        Task<bool> DescriptionSavePhoto(DescriptionPhoto photo);
        void DestinationUpdate(Destination desc);
        Task<bool> DeleteDescription(DestinationDescription desc);
        Task<DestinationDescriptionDto> GetDescriptionByIdAsync(int id);
        void DescriptionUpdate(DestinationDescription destinationDescription);
        Task<DescriptionPhoto> GetDescriptionPhotoByDescriptionIdAsync(int descriptionId);
        void DescriptionDeletePhoto(DescriptionPhoto photo);

    }
}
