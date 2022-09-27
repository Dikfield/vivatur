using API.Dtos;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IPromotion
    {
        Task<bool> SaveAllAsync();
        Task<IEnumerable<PromotionDto>> GetAllAsync();
        Task<PromotionDto> GetByNameAsync(string name);
        Task<PromotionDto> GetPromotionByIdAsync(int id);
        Task<PromotionPhoto> GetPhotoByIdAsync(int id);
        Task<bool> SavePhoto(PromotionPhoto photo);
        void DeletePhoto(PromotionPhoto photo);
        public Task<bool> DeletePromotion(Promotion dest);
        void Register(Promotion dest);
        void RegisterDescription(PromotionDescription desc);
        Task<bool> DescriptionSavePhoto(PromotionDescriptionPhoto photo);
        void PromotionUpdate(Promotion desc);
        Task<bool> DeleteDescription(PromotionDescription desc);
        Task<PromotionDescriptionDto> GetDescriptionByIdAsync(int id);
        void DescriptionUpdate(PromotionDescription destinationDescription);
        Task<PromotionDescriptionPhoto> GetDescriptionPhotoByDescriptionIdAsync(int descriptionId);
        void DescriptionDeletePhoto(PromotionDescriptionPhoto photo);

    }
}
