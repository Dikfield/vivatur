using API.Dtos;
using API.Entities;

namespace API.Interfaces
{
    public interface IAbout
    {
        public Task<AboutDto> GetAbout();
        public Task<About> GetAboutOrigin();
        public void Update(About about);
        public Task<bool> SaveAllAsync();
        public Task<VivaPhoto> GetPhotoByIdAsync(int id);
        public Task<bool> SavePhoto(VivaPhoto photo);
        public void DeletePhoto(VivaPhoto photo);
        
    }
}
