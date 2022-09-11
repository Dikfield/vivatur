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
        Task<Destination> GetByNameAsync(string name);
        Task<Photo> GetPhotoByIdAsync(int id);
        Task<bool> SavePhoto(Photo photo);
        void DeletePhoto(Photo photo);
        public void DeleteDestination(Destination dest);
        void Register(Destination dest);

    }
}
