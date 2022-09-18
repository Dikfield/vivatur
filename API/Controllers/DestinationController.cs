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
    public class DestinationController:BaseApiController
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

        [HttpGet("{name}", Name = "GetDestination")]
        public async Task<ActionResult> GetDestinationByName(string name)
        {
                        
            var destination = await _destinationRepo.GetByNameAsync(name);

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
           
            if(registerDestinationDto.Public ==1) dest.Public = true;
            else dest.Public = false;

            _destinationRepo.Register(dest);

            if (await _destinationRepo.SaveAllAsync())
                return Ok(registerDestinationDto);

            return BadRequest("Some error to save the new destination");


        }

        [HttpPost("add-photo/{name}")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file, string name)
        {

                var dest = await _destinationRepo.GetByNameAsync(name);
                var result = await _photoService.AddPhotoAsync(file);

                if (result.Error != null) return BadRequest(result.Error.Message);

                var photo = new Photo
                {
                    Url = result.SecureUrl.AbsoluteUri,
                    PublicId = result.PublicId,
                    DestinationId = dest.Id

                };

                if (dest.Photos.Count == 0)
                {
                    photo.IsMain = true;
                }

                
                await _destinationRepo.SavePhoto(photo);

                if (await _destinationRepo.SaveAllAsync())
                    return CreatedAtRoute("GetDestination", new { Name = dest.Name }, _mapper.Map<PhotoDto>(photo));



                return BadRequest("Problem Adding photo");
            

        }

        [HttpDelete("delete/{name}")]
        public async Task<ActionResult> DeleteDestination(string name)
        {
            var dest = await _destinationRepo.GetByNameAsync(name);

            if (dest == null) return NotFound("Name not found");

            var desti = new Destination();

            _mapper.Map(dest, desti);

            if (await _destinationRepo.DeleteDestination(desti)) return Ok($"{dest.Name} deleted");

            

            return BadRequest("same error in the system");




        }


        [HttpPut("set-main-photo/{name}/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId, string name)
        {
            var dest = await _destinationRepo.GetByNameAsync(name);

            var photo = await _destinationRepo.GetPhotoByIdAsync(photoId);

            if (photo.IsMain) return BadRequest("This is already a main photo");
            var currentMain = dest.Photos.FirstOrDefault(x => x.IsMain == true);

            if(currentMain !=null) currentMain.IsMain = false;
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
                if(result.Error != null) return BadRequest(result.Error);
            }

            _destinationRepo.DeletePhoto(photo);

            if (await _destinationRepo.SaveAllAsync()) return Ok();

            return BadRequest("Failed to delete the photo");
        }

        [HttpPut("{name}")]
        public async Task<ActionResult>UpdateDestination(DestinationUpdateDto destinationUpdateDto, string name)
        {
            var dest = await _destinationRepo.GetByNameAsync(name);

            if(dest == null) return NotFound("Try another name");

            var desti = new Destination();

            _mapper.Map(dest, desti);

            _mapper.Map(destinationUpdateDto, desti);

            _destinationRepo.Update(desti);

            if(await _destinationRepo.SaveAllAsync()) return Ok();

            return BadRequest("Failed to update destination");
        }

    }
}
