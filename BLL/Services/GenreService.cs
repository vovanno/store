using BLL.Interfaces;
using DAL.Interfaces;
using Domain.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unit;
        public GenreService(IUnitOfWork unit)
        {
            _unit = unit;
        }
        public async Task<int> Create(Genre genre)
        {
            if (genre == null)
                throw new ArgumentNullException(nameof(genre));
            var createdEntity = await _unit.GenreRepository.Add(genre);
            await _unit.Commit();
            return createdEntity.GenreId;
        }

        public async Task Edit(int genreId, Genre genre)
        {
            await _unit.GenreRepository.Update(genreId, genre);
            await _unit.Commit();
        }

        public async Task Delete(int id)
        {
            await _unit.GenreRepository.Delete(id);
            await _unit.Commit();
        }

        public async Task<Genre> GetById(int id)
        {
            var result = await _unit.GenreRepository.GetById(id);
            return result;
        }

        public async Task<IList<Genre>> GetAll()
        {
            var result = await _unit.GenreRepository.GetAll();
            return result.ToList();
        }

        public async Task<IList<GameDto>> GamesWithGenreId(int genreId)
        {
            var games = await _unit.GenreRepository.GamesWithGenreId(genreId);
            var genres = await _unit.GenreRepository.GetAll();
            var platforms = await _unit.PlatformRepository.GetAll();

            var result = games.Select(p =>
            {
                var genresIds = p.GameGenres.Select(x => x.GenreId);
                var platformsIds = p.PlatformTypes.Select(x => x.PlatformTypeId);

                return new GameDto
                {
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    DateOfAdding = p.DateOfAdding,
                    AmountOfViews = p.AmountOfViews,
                    GameId = p.GameId,
                    Publisher = p.Publisher,
                    GameGenres = genres.Where(x => genresIds.Contains(x.GenreId)).ToList(),
                    PlatformTypes = platforms.Where(x => platformsIds.Contains(x.PlatformTypeId)).ToList()
                };
            }).ToList();

            return result;
        }
    }
}
