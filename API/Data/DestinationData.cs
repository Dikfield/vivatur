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

        public async Task<DestinationPhoto> GetPhotoByIdAsync(int id)
        {
            return await _context.DestinationPhotos
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

        }

        public async Task<DescriptionPhoto> GetDescriptionPhotoByDescriptionIdAsync(int descriptionId)
        {
            return await _context.DescriptionPhotos
                .Where(x => x.DescriptionId == descriptionId)
                .SingleOrDefaultAsync();

        }

        public async Task<DestinationDto> GetByNameAsync(string name)
        {
            return await _context.Destinations
                .ProjectTo<DestinationDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(x => x.Name == name);

        }

        public async Task<DestinationDescriptionDto> GetDescriptionByIdAsync(int id)
        {
            return await _context.DestinationDescriptions
                .ProjectTo<DestinationDescriptionDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(x => x.Id == id);

        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task<bool> SavePhoto(DestinationPhoto photo)
        {
            await _context.DestinationPhotos.AddAsync(photo);
            return await _context.SaveChangesAsync() >= 0;

        }

        public async Task<bool> DescriptionSavePhoto(DescriptionPhoto photo)
        {
            await _context.DescriptionPhotos.AddAsync(photo);

            return await _context.SaveChangesAsync() >= 0;

        }


        public void DestinationUpdate(Destination dest)
        {
            _context.Entry(dest).State = EntityState.Modified;
        }

        public void DescriptionUpdate(DestinationDescription desc)
        {
            _context.Entry(desc).State = EntityState.Modified;
        }

        public void DeletePhoto(DestinationPhoto photo)
        {
            _context.DestinationPhotos.Remove(photo);
        }

        public void DescriptionDeletePhoto(DescriptionPhoto photo)
        {
            _context.DescriptionPhotos.Remove(photo);
        }
        public async Task<bool> DeleteDestination(Destination dest)
        {
            var count = dest.DestinationPhotos.Count;

            foreach (var photo in dest.DestinationPhotos)
                await _photoService.DeletePhotoAsync(photo.PublicId);

            _context.Destinations.Remove(dest);

            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<bool> DeleteDescription(DestinationDescription desc)
        {
            _context.DestinationDescriptions.Remove(desc);

            return await _context.SaveChangesAsync() > 0;

        }

        public void Register(Destination dest)
        {
            _context.Destinations.Add(dest);
        }

        public void RegisterDescription(DestinationDescription desc)
        {
            _context.DestinationDescriptions.Add(desc);
        }
    }
}
