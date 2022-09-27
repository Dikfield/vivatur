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
    public class PromotionController : BaseApiController
    {

        private readonly IPhotoService _photoService;
        private readonly IMapper _mapper;
        private readonly IPromotion _promotionRepository;

        public PromotionController(IPhotoService photoService, IMapper mapper, IPromotion promotionRepository)
        {
            _photoService = photoService;
            _mapper = mapper;
            _promotionRepository = promotionRepository;
        }

        [HttpGet("{id}", Name = "GetPromotion")]
        public async Task<ActionResult> GetPromotionById(int id)
        {

            var promotion = await _promotionRepository.GetPromotionByIdAsync(id);

            if (promotion == null) return NotFound("please try another name");

            return Ok(promotion);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllDestinations()
        {
            var promotions = await _promotionRepository.GetAllAsync();

            return Ok(promotions);
        }

        [HttpPost]
        public async Task<ActionResult> RegisterDestination(RegisterPromotionDto registerPromotionDto)
        {
            if (await _promotionRepository.GetByNameAsync(registerPromotionDto.Name) != null) return BadRequest("This Destination is already stored");

            var dest = new Promotion();

            _mapper.Map(registerPromotionDto, dest);


            dest.Name = registerPromotionDto.Name.ToLower();

            if (registerPromotionDto.Public == 1) dest.Public = true;
            else dest.Public = false;

            _promotionRepository.Register(dest);

            if (await _promotionRepository.SaveAllAsync())
                return Ok(registerPromotionDto);

            return BadRequest("Some error to save the new destination");


        }

        [HttpPost("add-photo/{id}")]
        public async Task<ActionResult<DestinationPhoto>> AddPhoto(IFormFile file, int id)
        {

            var promo = await _promotionRepository.GetPromotionByIdAsync(id);
            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new PromotionPhoto
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
                PromotionId = promo.Id

            };

            if (promo.PromotionPhotos.Count == 0)
            {
                photo.IsMain = true;
            }


            await _promotionRepository.SavePhoto(photo);

            if (await _promotionRepository.SaveAllAsync())
                return CreatedAtRoute("GetPromotion", new { Id = promo.Id }, _mapper.Map<PromotionPhotoDto>(photo));

            return BadRequest("Problem Adding photo");

        }

        [HttpPost("description/add-photo/{descriptionId}")]
        public async Task<ActionResult<DescriptionPhoto>> DescriptionAddPhoto(IFormFile file, int descriptionId)
        {

            var desc = await _promotionRepository.GetDescriptionByIdAsync(descriptionId);

            if (desc.PhotoUrl != null) return BadRequest("There is already a photo there");
            
            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new PromotionDescriptionPhoto
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
                PromotionDescriptionId = desc.Id,

            };

            await _promotionRepository.DescriptionSavePhoto(photo);

            var desci = new PromotionDescription();

            _mapper.Map(desc, desci);

            desci.PromotionDescriptionPhoto = photo;

            _promotionRepository.DescriptionUpdate(desci);

            if (await _promotionRepository.SaveAllAsync())
                return Ok();

            return BadRequest("Problem Adding photo");

        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteDestination(int id)
        {
            var dest = await _promotionRepository.GetPromotionByIdAsync(id);

            if (dest == null) return NotFound("Name not found");

            var desti = new Promotion();

            _mapper.Map(dest, desti);

            if (await _promotionRepository.DeletePromotion(desti)) return Ok();


            return BadRequest("same error in the system");

        }

        [HttpDelete("description/delete/{id}")]
        public async Task<ActionResult> DeleteDescription(int id)
        {
            var desc = await _promotionRepository.GetDescriptionByIdAsync(id);

            if (desc == null) return NotFound("Name not found");

            var desci = new PromotionDescription();

            _mapper.Map(desc, desci);

            if (await _promotionRepository.DeleteDescription(desci)) return Ok();


            return BadRequest("same error in the system");

        }

        [HttpPut("set-main-photo/{id}/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId, int id)
        {
            var dest = await _promotionRepository.GetPromotionByIdAsync(id);

            var photo = await _promotionRepository.GetPhotoByIdAsync(photoId);

            if (photo.IsMain) return BadRequest("This is already a main photo");
            var currentMain = dest.PromotionPhotos.FirstOrDefault(x => x.IsMain == true);


            if (currentMain != null) currentMain.IsMain = false;

            photo.IsMain = true;



            if (await _promotionRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to set a main photo");
        }

        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            var photo = await _promotionRepository.GetPhotoByIdAsync(photoId);


            if (photo == null) return NotFound();

            if (photo.IsMain) return BadRequest("You can not delete a main photo");

            if (photo.PublicId != null)
            {
                var result = await _photoService.DeletePhotoAsync(photo.PublicId);
                if (result.Error != null) return BadRequest(result.Error);
            }

            _promotionRepository.DeletePhoto(photo);

            if (await _promotionRepository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to delete the photo");
        }

        [HttpDelete("Description/delete-photo/{descriptionId}")]
        public async Task<ActionResult> DeletePhotoDescription(int descriptionId)
        {
            var photo = await _promotionRepository.GetDescriptionPhotoByDescriptionIdAsync(descriptionId);

            if (photo == null) return NotFound();


            if (photo.PublicId != null)
            {
                var result = await _photoService.DeletePhotoAsync(photo.PublicId);
                if (result.Error != null) return BadRequest(result.Error);
            }

            _promotionRepository.DescriptionDeletePhoto(photo);

            if (await _promotionRepository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to delete the photo");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDestination(PromotionUpdateDto promotionUpdateDto, int id)
        {
            var dest = await _promotionRepository.GetPromotionByIdAsync(id);

            if (dest == null) return NotFound("Try another name");

            var desti = new Promotion();

            _mapper.Map(dest, desti);

            _mapper.Map(promotionUpdateDto, desti);

            _promotionRepository.PromotionUpdate(desti);

            if (await _promotionRepository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to update destination");
        }

        [HttpPut("description/update/{id}")]
        public async Task<ActionResult> UpdateDescriptionn(PromotionDescriptionUpdateDto promotionDescriptionUpdateDto, int id)
        {
            var desc = await _promotionRepository.GetDescriptionByIdAsync(id);

            if (desc == null) return NotFound("Try another description id");

            var desti = new PromotionDescription();

            _mapper.Map(desc, desti);

            _mapper.Map(promotionDescriptionUpdateDto, desti);

            _promotionRepository.DescriptionUpdate(desti);

            if (await _promotionRepository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to update description");
        }

        [HttpPost("description/registerwp/{id}")]
        public async Task<ActionResult> RegisterDescriptionwp([FromForm] RegisterPromotionDescriptionDto registerPromotionDescriptionDto, int id)
        {
            var dest = await _promotionRepository.GetPromotionByIdAsync(id);


            var desc = new PromotionDescription();

            _mapper.Map(registerPromotionDescriptionDto, desc);

            _promotionRepository.RegisterDescription(desc);
            desc.PromotionId = dest.Id;

            if (!await _promotionRepository.SaveAllAsync()) return BadRequest("Some error to save the new destination");

            var result = await _photoService.AddPhotoAsync(registerPromotionDescriptionDto.File);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new PromotionDescriptionPhoto
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
                PromotionDescriptionId = desc.Id

            };

            _promotionRepository.DescriptionUpdate(desc);

            await _promotionRepository.DescriptionSavePhoto(photo);

            if (await _promotionRepository.SaveAllAsync())
                return Ok();

            return BadRequest("Problem Adding photo");
        }

        [HttpPost("description/register/{id}")]
        public async Task<ActionResult> RegisterDescription(RegisterPromotionDescriptionDto registerPromotionDescriptionDto, int id)
        {
            var dest = await _promotionRepository.GetPromotionByIdAsync(id);

            var desc = new PromotionDescription();

            _mapper.Map(registerPromotionDescriptionDto, desc);

            _promotionRepository.RegisterDescription(desc);
            desc.PromotionId = dest.Id;

            if (!await _promotionRepository.SaveAllAsync()) return BadRequest("Some error to save the new destination");

            return Ok();
        }

    }
}
