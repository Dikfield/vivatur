using API.Dtos;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace API.Data
{
    public class PromotionData : IPromotion
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public PromotionData(DataContext context, IMapper mapper, IPhotoService photoService)
        {
            _context = context;
            _mapper = mapper;
            _photoService = photoService;
        }

        public async Task<IEnumerable<PromotionDto>> GetAllAsync()
        {
            return await _context.Promotions
                .ProjectTo<PromotionDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<PromotionPhoto> GetPhotoByIdAsync(int id)
        {
            return await _context.PromotionPhotos
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

        }

        public async Task<PromotionDto> GetByNameAsync(string name)
        {
            return await _context.Promotions
                .ProjectTo<PromotionDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(x => x.Name == name);

        }

        public async Task<PromotionDto> GetPromotionByIdAsync(int id)
        {
            return await _context.Promotions
                .ProjectTo<PromotionDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(x => x.Id == id);

        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task<bool> SavePhoto(PromotionPhoto photo)
        {
            await _context.PromotionPhotos.AddAsync(photo);
            return await _context.SaveChangesAsync() >= 0;

        }


        public void PromotionUpdate(Promotion dest)
        {
            _context.Entry(dest).State = EntityState.Modified;
        }


        public void DeletePhoto(PromotionPhoto photo)
        {
            _context.PromotionPhotos.Remove(photo);
        }

        public async Task<bool> DeletePromotion(Promotion dest)
        {
            var count = dest.PromotionPhotos.Count;

            foreach (var photo in dest.PromotionPhotos)
                await _photoService.DeletePhotoAsync(photo.PublicId);

            _context.Promotions.Remove(dest);

            return await _context.SaveChangesAsync() > 0;

        }

        public void Register(Promotion dest)
        {
            _context.Promotions.Add(dest);
        }

    }
}
