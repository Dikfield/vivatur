using API.Dtos;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AboutData : IAbout
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AboutData(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() >0;
        }

        public void Update(About about)
        {
            _context.Entry(about).State = EntityState.Modified;
        }

        public async Task<VivaPhoto> GetPhotoByIdAsync(int id)
        {
            return await _context.VivaPhotos
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

        public async Task<AboutDto> GetAbout()
        {
            return await _context.Abouts
                .ProjectTo<AboutDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }


    }
}
