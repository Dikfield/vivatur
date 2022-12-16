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
        void PromotionUpdate(Promotion desc);

    }
}
