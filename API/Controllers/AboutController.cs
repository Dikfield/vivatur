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
        private readonly DataContext _context;

        public AboutController(IAbout about, IMapper mapper, IPhotoService photoService, DataContext context)
        {
            _about = about;
            _mapper = mapper;
            _photoService = photoService;
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> RegisterAbout(RegisterAboutDto registerAboutDto)
        {
            var about = _mapper.Map<About>(registerAboutDto);

            _context.Abouts.Add(about);

            await _context.SaveChangesAsync();

            return Ok(registerAboutDto);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAbout(AboutUpdateDto aboutUpdateDto)
        {
            var about = await _about.GetAboutOrigin();

            about.Name = aboutUpdateDto.Name;
            about.Description = aboutUpdateDto.Description;
            about.Information = aboutUpdateDto.Information;


            _mapper.Map(aboutUpdateDto, about);

            _about.Update(about);

            if (await _about.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update user");
        }

        [HttpGet(Name = "GetAbout")]
        public async Task<ActionResult<About>> GetAbout()
        {

            var about = await _about.GetAbout();

            return Ok(about);
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {

            var about = await _about.GetAboutOrigin();


            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new VivaPhoto
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId

            };

            if (about.VivaPhotos.Count == 0)
            {
                photo.IsMain = true;
            }


            about.VivaPhotos.Add(photo);

            if (await _about.SaveAllAsync())
                return CreatedAtRoute("GetAbout", _mapper.Map<VivaPhotoDto>(photo));



            return BadRequest("Problem Adding photo");


        }

        [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId)
        {
            var about = await _about.GetAboutOrigin();

            var photo = await _about.GetPhotoByIdAsync(photoId);

            if (photo.IsMain) return BadRequest("This is already a main photo");
            var currentMain = about.VivaPhotos.FirstOrDefault(x => x.IsMain == true);

            if (currentMain != null) currentMain.IsMain = false;
            photo.IsMain = true;

            if (await _about.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to set a main photo");
        }

        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            var photo = await _about.GetPhotoByIdAsync(photoId);


            if (photo == null) return NotFound();

            if (photo.IsMain) return BadRequest("You can not delete a main photo");

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
