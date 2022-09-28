using API.Data;
using API.Dtos;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AboutController:BaseApiController
    {
        private readonly IAbout _about;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        
        public AboutController(IAbout about, IMapper mapper, IPhotoService photoService)
        {
            _about = about;
            _mapper = mapper;
            _photoService = photoService;
        }

        [HttpPost]
        public async Task<ActionResult> RegisterAbout(RegisterAboutDto registerAboutDto)
        {
            var about = _mapper.Map<About>(registerAboutDto);

            _about.Register(about);

            if (!await _about.SaveAllAsync()) return BadRequest("Error saving about");

            return Ok(registerAboutDto);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteAbout(int id)
        {
            var dest = await _about.GetAboutById(id);

            if (dest == null) return NotFound("Name not found");

            var desti = new About();

            _mapper.Map(dest, desti);

            if (await _about.DeleteAbout(desti)) return Ok($"{desti.Title} deleted");


            return BadRequest("same error in the system");

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAbout(AboutUpdateDto aboutUpdateDto, int id)
        {
            var about = await _about.GetAboutById(id);

            if (about == null) return NotFound("Try another name");

            var abouti = new About();

            _mapper.Map(about, abouti);

            _mapper.Map(aboutUpdateDto, abouti);

            _about.Update(abouti);

            if (await _about.SaveAllAsync()) return Ok();

            return BadRequest("Failed to update destination");
        }

        [HttpGet(Name = "GetAbout")]
        public async Task<ActionResult> GetAbout()
        {

            var about = await _about.GetAbout();
            return Ok(about);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAboutById(int id)
        {

            var about = await _about.GetAboutById(id);
            return Ok(about);
        }

        [HttpPost("add-photo/{id}")]
        public async Task<ActionResult<VivaPhotoDto>> AddPhoto(IFormFile file, int id)
        {

            var about = await _about.GetAboutById(id);

            if (about == null) return BadRequest("id does not exist");

            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new VivaPhoto
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
                AboutId = id,
                VivaPic = true,
                IsMain = false


            };

            
            about.VivaPhotos.Add(photo);
            if (await _about.AboutSavePhoto(photo))
                return CreatedAtRoute("GetAbout", _mapper.Map<VivaPhotoDto>(photo));

            
            return BadRequest("Problem Adding photo");


        }

        [HttpPut("set-type-photo/{photoId}")]
        public async Task<ActionResult> SetPhotoType(int photoId)
        {
            var photo = await _about.GetPhotoByIdAsync(photoId);

            if (photo == null) return BadRequest("does not exist");

            photo.VivaPic = !photo.VivaPic;

            if (await _about.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to change the type");
        }

        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            var photo = await _about.GetPhotoByIdAsync(photoId);


            if (photo == null) return NotFound();


            if (photo.PublicId != null)
            {
                var result = await _photoService.DeletePhotoAsync(photo.PublicId);
                if (result.Error != null) return BadRequest(result.Error);
            }

            _about.DeletePhoto(photo);

            if (await _about.SaveAllAsync()) return Ok();

            return BadRequest("Failed to delete the photo");
        }
    }
}
