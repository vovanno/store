using AutoFixture;
using BLL.DTO;
using BLL.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace BLLTests
{
    public class GenreServiceTests
    {
        private readonly Mock<IGenreService> _mock;
        private const int NegativeValue = -1;

        public GenreServiceTests()
        {
            _mock = new Mock<IGenreService>();
        }

        [Fact]
        public void GamesWithGenreId_with_idArgument_less_than_null_should_throw_ArgumentException()
        {
            //arrange
            _mock.Setup(p => p.GamesWithGenreId(NegativeValue)).Throws<ArgumentException>();
            //act
            void Act() => _mock.Object.GamesWithGenreId(NegativeValue);
            //assert
            Assert.Throws<ArgumentException>(() => { Act(); });
        }

        [Fact]
        public void GamesWithGenreId_with_idArgument_that_exist_should_return_list_of_entities()
        {
            //arrange
            var fixture = new Fixture();
            var gamesList = new List<GameDto>();
            fixture.AddManyTo(gamesList);
            _mock.Setup(p => p.GamesWithGenreId(3)).ReturnsAsync(gamesList);
            //act
            var result = _mock.Object.GamesWithGenreId(3);
            //assert
            Assert.Equal(gamesList, result.Result);
        }
    }
}
