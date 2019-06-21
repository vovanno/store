using AutoFixture;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Moq;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;
using WebApi.VIewDto;
using Xunit;

namespace WebApiTests
{
    public class GamesControllerTests
    {
        private readonly Mock<IGameService> _gameService;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<GameController> _controller;

        public GamesControllerTests()
        {
            _controller = new Mock<GameController>();
            _mapper = new Mock<IMapper>();
            _gameService = new Mock<IGameService>();
        }

        [Fact]
        public void GamesController_()
        {
            //arrange
            var fixture = new Fixture();
            var games = new List<GameDto>();
            fixture.AddManyTo(games);
            _gameService.Setup(p => p.GetAll()).ReturnsAsync(games);
            var controller = new GameController(_gameService.Object, _mapper.Object);
            //act
            var actual = controller.Get();
            //assert
        }
    }
}
