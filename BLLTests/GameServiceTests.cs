using AutoFixture;
using BLL.DTO;
using BLL.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace BLLTests
{
    public class GameServiceTests
    {
        private readonly Mock<IGameService> _mock;
        private const int NegativeValue = -1;
        public GameServiceTests()
        {
            _mock = new Mock<IGameService>();
        }

        [Fact]
        public void GetGameGenres_with_argument_less_than_0_should_throw_ArgumentException()
        {
            //arrange
            _mock.Setup(p => p.GetGameGenres(NegativeValue)).Throws<ArgumentException>();
            //act
            void Act() => _mock.Object.GetGameGenres(NegativeValue);
            //assert
            Assert.Throws<ArgumentException>(() => { Act(); });
        }

        [Fact]
        public async void GetGameGenres_with_existentId_should_return_list_with_genres_of_entity_with_such_id()
        {
            //arrange
            var fixture = new Fixture();
            var genres = new List<GenreDto>();
            var game = fixture.Create<GameDto>();
            var necessaryGenres = new List<GenreDto>();
            fixture.AddManyTo(genres);
            for (var i = 0; i < genres.Count; i += 2)
            {
                necessaryGenres.Add(genres[i]);
            }
            _mock.Setup(p => p.GetGameGenres(game.GameId)).ReturnsAsync(necessaryGenres);
            //act
            var result = await _mock.Object.GetGameGenres(game.GameId);
            //assert
            Assert.Equal(necessaryGenres, result);
        }

        [Fact]
        public void LeaveComment_with_first_argument_less_than_0_should_throw_ArgumentException()
        {
            //arrange
            var comment = new CommentDto();
            _mock.Setup(p => p.LeaveComment(NegativeValue, comment)).Throws<ArgumentException>();
            //act
            void Act() => _mock.Object.LeaveComment(NegativeValue, comment);
            //assert
            Assert.Throws<ArgumentException>(() => { Act(); });
        }

        [Fact]
        public void LeaveComment_with_second_null_argument_should_throw_ArgumentNullException()
        {
            //arrange
            var comment = new CommentDto();
            _mock.Setup(p => p.LeaveComment(NegativeValue, comment)).Throws<ArgumentNullException>();
            //act
            void Act() => _mock.Object.LeaveComment(NegativeValue, comment);
            //assert
            Assert.Throws<ArgumentNullException>(() => { Act(); });
        }

        [Fact]
        public async void GetCommentsByGameId_with_existentId_should_return_list_with_comments_of_entity_with_such_id()
        {
            //arrange
            var fixture = new Fixture();
            var comments = new List<CommentDto>();
            var game = fixture.Create<GameDto>();
            var necessaryComments = new List<CommentDto>();
            fixture.AddManyTo(comments);
            for (var i = 0; i < comments.Count; i += 2)
            {
                necessaryComments.Add(comments[i]);
            }
            _mock.Setup(p => p.GetCommentsByGameId(game.GameId)).ReturnsAsync(necessaryComments);
            //act
            var result = await _mock.Object.GetCommentsByGameId(game.GameId);
            //assert
            Assert.Equal(necessaryComments, result);
        }

        [Fact]
        public void GetCommentsByGameId_with_argument_less_than_0_should_throw_ArgumentException()
        {
            //arrange
            _mock.Setup(p => p.GetCommentsByGameId(NegativeValue)).Throws<ArgumentException>();
            //act
            void Act() => _mock.Object.GetCommentsByGameId(NegativeValue);
            //assert
            Assert.Throws<ArgumentException>(() => { Act(); });
        }
    }
}
