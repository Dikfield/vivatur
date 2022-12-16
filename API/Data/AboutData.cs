using API.Dtos;
using API.Entities;
using API.Interfaces;
using API.Migrations;
using API.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AboutData : IAbout
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public AboutData(DataContext context, IMapper mapper, IPhotoService photoService)
        {
            _context = context;
            _mapper = mapper;
            _photoService = photoService;
        }


        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(About about)
        {
            _context.Entry(about).State = EntityState.Modified;
        }

        public async Task<bool> DeleteAbout(About about)
        {
            var count = about.VivaPhotos.Count();

            foreach (var photo in about.VivaPhotos)
                await _photoService.DeletePhotoAsync(photo.PublicId);

            _context.Abouts.Remove(about);

            return await _context.SaveChangesAsync() > 0;

        }
        public async Task<bool> AboutSavePhoto(VivaPhoto photo)
        {
            await _context.VivaPhotos.AddAsync(photo);

            return await _context.SaveChangesAsync() >= 0;

        }

        public async Task<bool> FeedSavePhoto(FeedbackPhoto photo)
        {
            await _context.FeedbackPhotos.AddAsync(photo);

            return await _context.SaveChangesAsync() >= 0;

        }

        public async Task<VivaPhoto> GetPhotoByIdAsync(int id)
        {
            return await _context.VivaPhotos
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();
        }

        public async Task<FeedbackPhoto> GetFeedPhotoByIdAsync(int id)
        {
            return await _context.FeedbackPhotos
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();
        }

        public async Task<bool> SavePhoto(VivaPhoto vivaPhoto)
        {
            await _context.VivaPhotos.AddAsync(vivaPhoto);
            return await _context.SaveChangesAsync() >= 0;
        }


        public void DeletePhoto(VivaPhoto photo)
        {
            _context.VivaPhotos.Remove(photo);
        }

        public async Task<About> GetAboutOrigin()
        {
            return await _context.Abouts
                .Include(x => x.VivaPhotos)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<AboutDto>> GetAbout()
        {
            return await _context.Abouts
                .ProjectTo<AboutDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<AboutDto> GetAboutById(int id)
        {
            return await _context.Abouts
                .ProjectTo<AboutDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public void Register(About about)
        {
            _context.Abouts.Add(about);
        }

        public void RegisterFeedback(Feedback feedback)
        {
            _context.Feedbacks.Add(feedback);
        }

        public void UpdateFeedback(Feedback feedback)
        {
            _context.Entry(feedback).State = EntityState.Modified;
        }

        public async Task<FeedbackDto> GetFeedById(int id)
        {
            return await _context.Feedbacks
                .ProjectTo<FeedbackDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<FeedbackDto>> GetFeed()
        {
            return await _context.Feedbacks
                .ProjectTo<FeedbackDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public void DeleteFeedPhoto(FeedbackPhoto feedbackPhoto)
        {
            _context.FeedbackPhotos.Remove(feedbackPhoto);

        }

        public async Task<bool> DeleteFeed(Feedback feedback)
        {
            if (feedback.FeedbackPhoto != null)
            {
                await _photoService.DeletePhotoAsync(feedback.FeedbackPhoto.PublicId);

                _context.FeedbackPhotos.Remove(feedback.FeedbackPhoto);
            }

            _context.Feedbacks.Remove(feedback);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
