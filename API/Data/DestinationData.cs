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
    public class DestinationData : IDestination
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public DestinationData(DataContext context, IMapper mapper, IPhotoService photoService)
        {
            _context = context;
            _mapper = mapper;
            _photoService = photoService;
        }

        public async Task<IEnumerable<DestinationDto>> GetAllAsync()
        {
            return await _context.Destinations
                .ProjectTo<DestinationDto>(_mapper.ConfigurationProvider)
                .ToListAsync(); 
        }

        public async Task<Photo> GetPhotoByIdAsync(int id)
        {
            return await _context.Photos
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();
        }

        public async Task<DestinationDto> GetByNameAsync(string name)
        {
            return await _context.Destinations
                .ProjectTo<DestinationDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(x => x.Name == name);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task<bool> SavePhoto(Photo photo)
        {
            await _context.Photos.AddAsync(photo);
            return await _context.SaveChangesAsync() >= 0;
        }

        public void Update(Destination dest)
        {
            _context.Entry(dest).State= EntityState.Modified;
        }

        public void DeletePhoto(Photo photo)
        {
            _context.Photos.Remove(photo);
        }
        public async Task<bool> DeleteDestination(Destination dest)
        {
            var count = dest.Photos.Count;

            foreach (var photo in dest.Photos)
                await _photoService.DeletePhotoAsync(photo.PublicId);

            _context.Destinations.Remove(dest);

            return await _context.SaveChangesAsync() > 0;
                                                
        }

        public void Register(Destination dest)
        {
            _context.Destinations.Add(dest);
        }
    }
}
