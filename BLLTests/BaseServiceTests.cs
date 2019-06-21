using AutoFixture;
using BLL.DTO;
using BLL.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace BLLTests
{
    public class BaseServiceTests
    {
        private readonly Mock<IBaseService<GameDto>> _mock;
        private const int NegativeValue = -1;
        public BaseServiceTests()
        {
            _mock = new Mock<IBaseService<GameDto>>();
        }
        [Fact]
        public async void AddMethod_after_first_adding_should_return_1()
        {
            //arrange
            var game = new GameDto();
            _mock.Setup(p => p.Create(game)).ReturnsAsync(1);
            //act
            var result = await _mock.Object.Create(game);
            //assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void AddMethod_with_null_argument_should_throw_ArgumentNullException()
        {
            //arrange
            _mock.Setup(p => p.Create(null)).Throws<ArgumentNullException>();
            //act
            void Act() => _mock.Object.Create(null);
            //assert
            Assert.Throws<ArgumentNullException>(() => { Act(); });
        }

        [Fact]
        public void EditMethod_with_idArgument_less_than_1_should_return_ArgumentException()
        {
            //arrange
            var game = new GameDto { GameId = NegativeValue };
            _mock.Setup(p => p.Edit(game)).Throws<ArgumentException>();
            //act
            void Act() => _mock.Object.Edit(game);
            //assert
            Assert.Throws<ArgumentException>(() => { Act(); });
        }

        [Fact]
        public void EditMethod_with_null_argument_should_throw_ArgumentNullException()
        {
            //arrange
            _mock.Setup(p => p.Edit(null)).Throws<ArgumentNullException>();
            //act
            void Act() => _mock.Object.Edit(null);
            //assert
            Assert.Throws<ArgumentNullException>(() => { Act(); });
        }

        [Fact]
        public void EditMethod_with_id_that_doesNot_exist_should_return_ArgumentException()
        {
            //arrange
            var fixture = new Fixture();
            var list = new List<GameDto>();
            fixture.AddManyTo(list);
            var game = fixture.Create<GameDto>();
            _mock.Setup(p => p.Edit(game)).Throws<ArgumentException>();
            //act
            void Act() => _mock.Object.Edit(game);
            //assert
            Assert.Throws<ArgumentException>(() => { Act(); });
        }

        [Fact]
        public void GetById_method_with_id_from_the_existent_pool_should_return_entity_with_this_id()
        {
            //arrange
            var fixture = new Fixture();
            var list = new List<GameDto>();
            fixture.AddManyTo(list);
            _mock.Setup(p => p.GetById(list[0].GameId)).ReturnsAsync(list[0]);
            //act
            var result = _mock.Object.GetById(list[0].GameId);
            //assert
            Assert.Equal(list[0], result.Result);
        }

        [Fact]
        public void GetById_Method_with_idArgument_less_than_1_should_return_ArgumentException()
        {
            //arrange
            _mock.Setup(p => p.GetById(NegativeValue)).Throws<ArgumentException>();
            //act
            void Act() => _mock.Object.GetById(NegativeValue);
            //assert
            Assert.Throws<ArgumentException>(() => { Act(); });
        }

        [Fact]
        public void Delete_Method_with_idArgument_less_than_1_should_return_ArgumentException()
        {
            //arrange
            _mock.Setup(p => p.Delete(NegativeValue)).Throws<ArgumentException>();
            //act
            void Act() => _mock.Object.Delete(NegativeValue);
            //assert
            Assert.Throws<ArgumentException>(() => { Act(); });
        }

        [Fact]
        public void GetAll_method_should_return_the_whole_list_with_existent_entries()
        {
            //arrange
            var fixture = new Fixture();
            var list = new List<GameDto>();
            fixture.AddManyTo(list);
            _mock.Setup(p => p.GetAll()).ReturnsAsync(list);
            //act
            var result = _mock.Object.GetAll();
            //assert
            Assert.Equal(list, result.Result);
        }
    }
}
