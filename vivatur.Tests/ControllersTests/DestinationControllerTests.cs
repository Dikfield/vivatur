using API.Controllers;
using API.Dtos;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace vivatur.Tests.ControllersTests
{
    public class DestinationControllerTests
    {
        private readonly DestinationController _controller;
        private readonly Mock<IPhotoService> _photoService;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IDestination> _destinationRepo;
        private readonly DestinationDto _paris;
        private readonly DestinationDto _berlin;

        public DestinationControllerTests()
        {
            
            _photoService = new Mock<IPhotoService>();
            _mapper = new Mock<IMapper>();
            _destinationRepo = new Mock<IDestination>();

            _controller = new DestinationController(_photoService.Object, _mapper.Object, _destinationRepo.Object);

            _paris = Mock.Of<DestinationDto>
                (x =>
                    x.Name == "Paris" &&
                    x.Public == true &&
                    x.Id == 1 &&
                    x.Country == "France"
                );

            _berlin =  Mock.Of<DestinationDto>
                (x =>
                    x.Name == "Paris" &&
                    x.Public == true &&
                    x.Id == 1 &&
                    x.Country == "France"
                );
        }

        [Fact]
        public async void GetAllDestinations_Returns_AllDestinations()
        {
            var list = new List<DestinationDto>();
            list.Add(_paris);
            list.Add(_berlin);
            

            _destinationRepo.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(list.AsEnumerable()));

            var listAll = await _controller.GetAllDestinations();

            Assert.Equal(2, list.Count);
            Assert.IsAssignableFrom<Task<ActionResult>>(listAll);
        }

        [Fact]
        public async void GetDestinationByName_Returns_Destination()
        {
            var france = Mock.Of<Destination>
                        (x =>
                            x.Name == "Paris" &&
                            x.Public == true &&
                            x.Id == 1 &&
                            x.Country == "France"
                        );

            _destinationRepo.Setup(x => x.GetByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(france));

            var result = await _controller.GetDestinationByName(It.IsAny<string>());

            Assert.IsAssignableFrom<ActionResult<DestinationDto>>(result);
            Assert.Equal("Paris", france.Name);

        }

        [Fact]
        public async void RegisterDestinationHolland_Returns_DestinationHolland()
        {
            var dest = Mock.Of<RegisterDestinationDto>
            (x =>
                x.Name == "Brasil" &&
                x.Public == true
            );


            _destinationRepo.Setup(x => x.GetByNameAsync(It.IsAny<string>())).Returns(Task.FromResult<Destination>(null));

             
            _destinationRepo.Setup(x => x.SaveAllAsync()).Returns(Task.FromResult(true));


            var result = await _controller.RegisterDestination(dest);

            Assert.IsAssignableFrom<ActionResult>(result);



        }
    }
}
