using API.Dtos;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IDestination
    {
        public void Update(Destination dest);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<DestinationDto>> GetAllAsync();
        Task<DestinationDto> GetByNameAsync(string name);
        Task<DestinationPhoto> GetPhotoByIdAsync(int id);
        Task<bool> SavePhoto(DestinationPhoto photo);
        void DeletePhoto(DestinationPhoto photo);
        public Task<bool> DeleteDestination(Destination dest);
        void Register(Destination dest);
        void RegisterDescription(DestinationDescription desc);
        public Task<bool> DescriptionSavePhoto(DescriptionPhoto photo);

    }
}
