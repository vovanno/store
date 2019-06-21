using AutoFixture;
using BLL.DTO;
using BLL.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace BLLTests
{
    public class PublisherServiceTests
    {
        private readonly Mock<IPublisherService> _mock;
        private const int NegativeValue = -1;

        public PublisherServiceTests()
        {
            _mock = new Mock<IPublisherService>();
        }

        [Fact]
        public void GetGamesByPublisherId_with_idArgument_less_than_null_should_throw_ArgumentException()
        {
            //arrange
            _mock.Setup(p => p.GetGamesByPublisherId(NegativeValue)).Throws<ArgumentException>();
            //act
            void Act() => _mock.Object.GetGamesByPublisherId(NegativeValue);
            //assert
            Assert.Throws<ArgumentException>(() => { Act(); });
        }

        [Fact]
        public void GetGamesByPublisherId_with_with_idArgument_that_exist_should_return_list_of_entities()
        {
            //arrange
            var fixture = new Fixture();
            var gamesList = new List<GameDto>();
            fixture.AddManyTo(gamesList);
            _mock.Setup(p => p.GetGamesByPublisherId(3)).ReturnsAsync(gamesList);
            //act
            var result = _mock.Object.GetGamesByPublisherId(3);
            //assert
            Assert.Equal(gamesList, result.Result);
        }
    }
}
