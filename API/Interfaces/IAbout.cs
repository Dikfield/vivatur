using API.Dtos;
using API.Entities;
using System.Collections.Generic;

namespace API.Interfaces
{
    public interface IAbout
    {
        Task<IEnumerable<AboutDto>> GetAbout();
        Task<IEnumerable<FeedbackDto>> GetFeed();
        public Task<About> GetAboutOrigin();
        public void Update(About about);
        public void Register(About about);
        public Task<bool> SaveAllAsync();
        public Task<VivaPhoto> GetPhotoByIdAsync(int id);
        public Task<FeedbackPhoto> GetFeedPhotoByIdAsync(int id);
        public Task<bool> SavePhoto(VivaPhoto photo);
        public void DeletePhoto(VivaPhoto photo);
        Task<bool> DeleteAbout(About about);
        public void DeleteFeedPhoto(FeedbackPhoto feedbackPhoto);
        Task<bool> DeleteFeed(Feedback feedback);
        Task<AboutDto> GetAboutById(int id);
        Task<FeedbackDto> GetFeedById(int id);
        Task<bool> AboutSavePhoto(VivaPhoto photo);
        Task<bool> FeedSavePhoto(FeedbackPhoto photo);
        public void RegisterFeedback(Feedback feedback);
        public void UpdateFeedback(Feedback feedback);


    }
}
