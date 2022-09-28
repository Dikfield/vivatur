using API.Dtos;
using API.Entities;
using System.Collections.Generic;

namespace API.Interfaces
{
    public interface IAbout
    {
        Task<IEnumerable<AboutDto>> GetAbout();
        public Task<About> GetAboutOrigin();
        public void Update(About about);
        public void Register(About about);
        public Task<bool> SaveAllAsync();
        public Task<VivaPhoto> GetPhotoByIdAsync(int id);
        public Task<bool> SavePhoto(VivaPhoto photo);
        public void DeletePhoto(VivaPhoto photo);
        Task<bool> DeleteAbout(About about);
        Task<AboutDto> GetAboutById(int id);
        Task<bool> AboutSavePhoto(VivaPhoto photo);


    }
}
