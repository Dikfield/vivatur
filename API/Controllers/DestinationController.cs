using API.Data;
using API.Dtos;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class DestinationController : BaseApiController
    {

        private readonly IPhotoService _photoService;
        private readonly IMapper _mapper;
        private readonly IDestination _destinationRepo;

        public DestinationController(IPhotoService photoService, IMapper mapper, IDestination destinationRepo)
        {
            _photoService = photoService;
            _mapper = mapper;
            _destinationRepo = destinationRepo;
        }

        [HttpGet("{id}", Name = "GetDestination")]
        public async Task<ActionResult> GetDestinationById(int id)
        {

            var destination = await _destinationRepo.GetDestinationByIdAsync(id);

            if (destination == null) return NotFound("please try another name");

            return Ok(destination);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllDestinations()
        {
            var destinations = await _destinationRepo.GetAllAsync();

            return Ok(destinations);
        }

        [HttpPost]
        public async Task<ActionResult> RegisterDestination(RegisterDestinationDto registerDestinationDto)
        {
            if (await _destinationRepo.GetByNameAsync(registerDestinationDto.Name) != null) return BadRequest("This Destination is already stored");

            var dest = new Destination();

            _mapper.Map(registerDestinationDto, dest);


            dest.Name = registerDestinationDto.Name.ToLower();

            if (registerDestinationDto.Public == 1) dest.Public = true;
            else dest.Public = false;

            _destinationRepo.Register(dest);

            if (await _destinationRepo.SaveAllAsync())
                return Ok(registerDestinationDto);

            return BadRequest("Some error to save the new destination");


        }

        [HttpPost("add-photo/{id}")]
        public async Task<ActionResult<DestinationPhoto>> AddPhoto(IFormFile file, int id)
        {

            var dest = await _destinationRepo.GetDestinationByIdAsync(id);
            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new DestinationPhoto
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
                DestinationId = dest.Id

            };

            if (dest.DestinationPhotos.Count == 0)
            {
                photo.IsMain = true;
            }


            await _destinationRepo.SavePhoto(photo);

            if (await _destinationRepo.SaveAllAsync())
                return CreatedAtRoute("GetDestination", new { Id = dest.Id }, _mapper.Map<DestinationPhotoDto>(photo));

            return BadRequest("Problem Adding photo");

        }

        [HttpPost("description/add-photo/{descriptionId}")]
        public async Task<ActionResult<DescriptionPhoto>> DescriptionAddPhoto(IFormFile file, int descriptionId)
        {

            var desc = await _destinationRepo.GetDescriptionByIdAsync(descriptionId);

            if (desc.PhotoUrl != null) return BadRequest("There is already a photo there");
            
            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new DescriptionPhoto
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
                DescriptionId = desc.Id,

            };

            await _destinationRepo.DescriptionSavePhoto(photo);

            var desci = new DestinationDescription();

            _mapper.Map(desc, desci);

            desci.DescriptionPhoto = photo;

            _destinationRepo.DescriptionUpdate(desci);

            if (await _destinationRepo.SaveAllAsync())
                return Ok();

            return BadRequest("Problem Adding photo");

        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteDestination(int id)
        {
            var dest = await _destinationRepo.GetDestinationByIdAsync(id);

            if (dest == null) return NotFound("Name not found");

            var desti = new Destination();

            _mapper.Map(dest, desti);

            if (await _destinationRepo.DeleteDestination(desti)) return Ok();


            return BadRequest("same error in the system");

        }

        [HttpDelete("description/delete/{id}")]
        public async Task<ActionResult> DeleteDescription(int id)
        {
            var desc = await _destinationRepo.GetDescriptionByIdAsync(id);

            if (desc == null) return NotFound("Name not found");

            var desci = new DestinationDescription();

            _mapper.Map(desc, desci);

            if (await _destinationRepo.DeleteDescription(desci)) return Ok();


            return BadRequest("same error in the system");

        }

        [HttpPut("set-main-photo/{id}/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId, int id)
        {
            var dest = await _destinationRepo.GetDestinationByIdAsync(id);

            var photo = await _destinationRepo.GetPhotoByIdAsync(photoId);

            if (photo.IsMain) return BadRequest("This is already a main photo");
            var currentMain = dest.DestinationPhotos.FirstOrDefault(x => x.IsMain == true);


            if (currentMain != null) currentMain.IsMain = false;

            photo.IsMain = true;



            if (await _destinationRepo.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to set a main photo");
        }

        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            var photo = await _destinationRepo.GetPhotoByIdAsync(photoId);


            if (photo == null) return NotFound();

            if (photo.IsMain) return BadRequest("You can not delete a main photo");

            if (photo.PublicId != null)
            {
                var result = await _photoService.DeletePhotoAsync(photo.PublicId);
                if (result.Error != null) return BadRequest(result.Error);
            }

            _destinationRepo.DeletePhoto(photo);

            if (await _destinationRepo.SaveAllAsync()) return Ok();

            return BadRequest("Failed to delete the photo");
        }

        [HttpDelete("Description/delete-photo/{descriptionId}")]
        public async Task<ActionResult> DeletePhotoDescription(int descriptionId)
        {
            var photo = await _destinationRepo.GetDescriptionPhotoByDescriptionIdAsync(descriptionId);

            if (photo == null) return NotFound();


            if (photo.PublicId != null)
            {
                var result = await _photoService.DeletePhotoAsync(photo.PublicId);
                if (result.Error != null) return BadRequest(result.Error);
            }

            _destinationRepo.DescriptionDeletePhoto(photo);

            if (await _destinationRepo.SaveAllAsync()) return Ok();

            return BadRequest("Failed to delete the photo");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDestination(DestinationUpdateDto destinationUpdateDto, int id)
        {
            var dest = await _destinationRepo.GetDestinationByIdAsync(id);

            if (dest == null) return NotFound("Try another name");

            var desti = new Destination();

            _mapper.Map(dest, desti);

            _mapper.Map(destinationUpdateDto, desti);

            _destinationRepo.DestinationUpdate(desti);

            if (await _destinationRepo.SaveAllAsync()) return Ok();

            return BadRequest("Failed to update destination");
        }

        [HttpPut("description/update/{id}")]
        public async Task<ActionResult> UpdateDescriptionn(DestinationDescriptionUpdateDto destinationDescriptionUpdateDto, int id)
        {
            var desc = await _destinationRepo.GetDescriptionByIdAsync(id);

            if (desc == null) return NotFound("Try another description id");

            var desti = new DestinationDescription();

            _mapper.Map(desc, desti);

            _mapper.Map(destinationDescriptionUpdateDto, desti);

            _destinationRepo.DescriptionUpdate(desti);

            if (await _destinationRepo.SaveAllAsync()) return Ok();

            return BadRequest("Failed to update description");
        }

        [HttpPost("description/registerwp/{id}")]
        public async Task<ActionResult> RegisterDescriptionwp([FromForm] RegisterDestinationDescriptionDto registerDestinationDescriptionDto, int id)
        {
            var dest = await _destinationRepo.GetDestinationByIdAsync(id);


            var desc = new DestinationDescription();

            _mapper.Map(registerDestinationDescriptionDto, desc);

            _destinationRepo.RegisterDescription(desc);
            desc.DestinationId = dest.Id;

            if (!await _destinationRepo.SaveAllAsync()) return BadRequest("Some error to save the new destination");

            var result = await _photoService.AddPhotoAsync(registerDestinationDescriptionDto.File);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new DescriptionPhoto
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
                DescriptionId = desc.Id

            };

            _destinationRepo.DescriptionUpdate(desc);

            await _destinationRepo.DescriptionSavePhoto(photo);

            if (await _destinationRepo.SaveAllAsync())
                return Ok();

            return BadRequest("Problem Adding photo");
        }

        [HttpPost("description/register/{id}")]
        public async Task<ActionResult> RegisterDescription(RegisterDestinationDescriptionDto registerDestinationDescriptionDto, int id)
        {
            var dest = await _destinationRepo.GetDestinationByIdAsync(id);

            var desc = new DestinationDescription();

            _mapper.Map(registerDestinationDescriptionDto, desc);

            _destinationRepo.RegisterDescription(desc);
            desc.DestinationId = dest.Id;

            if (!await _destinationRepo.SaveAllAsync()) return BadRequest("Some error to save the new destination");

            return Ok();
        }

    }
}
