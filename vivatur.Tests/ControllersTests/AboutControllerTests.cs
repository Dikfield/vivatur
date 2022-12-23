using API.Controllers;
using API.Data;
using API.Dtos;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace vivatur.Tests.ControllersTests
{
    public class AboutControllerTests
    {
        private readonly AboutController _controller;
        private readonly Mock<IAbout> _about;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IPhotoService> _photoService;
        public AboutControllerTests()
        {
            _about = new Mock<IAbout>();
            _mapper = new Mock<IMapper>();
            _photoService = new Mock<IPhotoService>();
            
            _controller = new AboutController(_about.Object, _mapper.Object, _photoService.Object);
        }


        [Fact]
        public async void UpdateAbout_Returns_UpdatedAbout()
        {
            //var currentAbout = Mock.Of<About>(x=>x.Information == "sou linda" && x.Description == "SOu viviane" && x.Name=="vava");

            //_about.Setup(x => x.GetAboutOrigin()).Returns(Task.FromResult(currentAbout));


            //var newAbout = new AboutUpdateDto { Name = "viva", Description = "lulu", Information = "lala" };



            
            _about.Setup(x => x.SaveAllAsync()).Returns(Task.FromResult(true));

            //var result = await _controller.UpdateAbout(newAbout);

            
            //Assert.Null(Mock.Of<About>().Name);
        }

        [Fact]
        public async void GetAllAbout_Returns_AllAbout()
        {
            //var about = Mock.Of<AboutDto>(x => x.Information == "sou linda" && x.Description == "SOu viviane" && x.Name == "vava");

            //_about.Setup(_about => _about.GetAbout()).Returns(Task.FromResult(about));

            var result = await _controller.GetAbout();

            //Assert.Equal("vava", about.Name);
            Assert.IsAssignableFrom<ActionResult<About>>(result);
            Assert.IsType<ActionResult<About>>(result);
        }
    }
}
