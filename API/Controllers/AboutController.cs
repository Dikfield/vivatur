using API.Data;
using API.Dtos;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

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

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> RegisterAbout(RegisterAboutDto registerAboutDto)
        {
            var about = _mapper.Map<About>(registerAboutDto);

            _about.Register(about);

            if (!await _about.SaveAllAsync()) return BadRequest("Error saving about");

            return Ok(registerAboutDto);
        }

        [Authorize]
        [HttpPost("feed")]
        public async Task<ActionResult> RegisterFeedback(RegisterFeedDto registerFeedDto)
        {
            var feed= _mapper.Map<Feedback>(registerFeedDto);
            feed.AboutId = 2;

            _about.RegisterFeedback(feed);

            if (!await _about.SaveAllAsync()) return BadRequest("Error saving about");

            return Ok(feed);

         }

        [Authorize]
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteAbout(int id)
        {
            var about = await _about.GetAboutById(id);

            if (about == null) return NotFound("Name not found");

            var abouti = new About();

            _mapper.Map(about, abouti);

            if (await _about.DeleteAbout(abouti)) return Ok($"{abouti.Title} deleted");


            return BadRequest("same error in the system");

        }

        [Authorize]
        [HttpDelete("feed/delete/{id}")]
        public async Task<ActionResult> DeleteFeed(int id)
        {
            var feed = await _about.GetFeedById(id);

            if (feed == null) return NotFound("Name not found");

            var feedi = new Feedback();

            _mapper.Map(feed, feedi);

            if (await _about.DeleteFeed(feedi)) return Ok($"{feedi.Name} deleted");


            return BadRequest("same error in the system");

        }

        [Authorize]
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

        [Authorize]
        [HttpPut("feed/{id}")]
        public async Task<ActionResult> UpdateFeedback(FeedbackUpdateDto feedbackUpdateDto, int id)
        {
            var feed = await _about.GetFeedById(id);

            if (feed == null) return NotFound("Try another feedback");

            var feedi = new Feedback();

            _mapper.Map(feed, feedi);

            _mapper.Map(feedbackUpdateDto, feedi);

            _about.UpdateFeedback(feedi);

            if (await _about.SaveAllAsync()) return Ok();

            return BadRequest("Failed to update feedbacks");
        }

        [HttpGet(Name = "GetAbout")]
        public async Task<ActionResult> GetAbout()
        {

            var about = await _about.GetAbout();
            return Ok(about);
        }

        [HttpGet("feed/{id}")]
        public async Task<ActionResult> GetFeedById(int id)
        {

            var feed = await _about.GetFeedById(id);
            return Ok(feed);
        }

        [HttpGet("feed")]
        public async Task<ActionResult> GetFeed()
        {

            var feeds = await _about.GetFeed();
            return Ok(feeds);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAboutById(int id)
        {

            var about = await _about.GetAboutById(id);
            return Ok(about);
        }

        [Authorize]
        [HttpPost("feed/add-photo/{id}")]
        public async Task<ActionResult<FeedbackPhotoDto>> AddFeedPhoto(IFormFile file, int id)
        {

            var feed = await _about.GetFeedById(id);

            if (feed == null) return BadRequest("id does not exist");

            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new FeedbackPhoto
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
                IsMain = true,
                FeedbackId = feed.Id,
            };

            feed.FeedbackPhoto = photo;

            await _about.FeedSavePhoto(photo);
            

            var feedi = new Feedback();

            _mapper.Map(feed, feedi);

            _about.UpdateFeedback(feedi);

            if (await _about.SaveAllAsync()) return Ok(new {Id = photo.Id, Url = photo.Url, IsMain = photo.IsMain});

            return BadRequest("Failed to update feedbacks");


        }

        [Authorize]
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

        [Authorize]
        [HttpPut("set-type-photo/{photoId}")]
        public async Task<ActionResult> SetPhotoType(int photoId)
        {
            var photo = await _about.GetPhotoByIdAsync(photoId);

            if (photo == null) return BadRequest("does not exist");

            photo.VivaPic = !photo.VivaPic;

            if (await _about.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to change the type");
        }

        [Authorize]
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

        [Authorize]
        [HttpDelete("feed/delete-photo/{feedId}")]
        public async Task<ActionResult> DeleteFeedPhoto(int feedId)
        {
            var feed = await _about.GetFeedById(feedId);
            var photo = feed.FeedbackPhoto;


            if (photo == null) return NotFound();


            if (photo.PublicId != null)
            {
                var result = await _photoService.DeletePhotoAsync(photo.PublicId);
                if (result.Error != null) return BadRequest(result.Error);
            }
            
            feed.FeedbackPhoto = null;
            _about.DeleteFeedPhoto(photo);

            if (await _about.SaveAllAsync()) return Ok();

            return BadRequest("Failed to delete the photo");
        }
    }
}
