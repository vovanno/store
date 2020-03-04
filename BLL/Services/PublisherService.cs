using BLL.Interfaces;
using DAL.Exceptions;
using DAL.Interfaces;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dtos;

namespace BLL.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IUnitOfWork _unit;
        public PublisherService(IUnitOfWork unit)
        {
            _unit = unit;
        }
        public async Task<int> Create(Publisher publisher)
        {
            var resultPublisher = await _unit.PublisherRepository.Add(publisher);
            await _unit.Commit();
            return resultPublisher.PublisherId;
        }

        public async Task Edit(int publisherId, Publisher publisher)
        {
            await _unit.PublisherRepository.Update(publisherId, publisher);
            await _unit.Commit();
        }

        public async Task Delete(int id)
        {
            await _unit.PublisherRepository.Delete(id);
            await _unit.Commit();
        }

        public async Task<Publisher> GetById(int id)
        {
            var result = await _unit.PublisherRepository.GetById(id);
            if (result == null)
                throw new EntryNotFoundException($"Entry with id = {id} does not found");
            return result;
        }

        public async Task<List<Publisher>> GetAll()
        {
            var result = await _unit.PublisherRepository.GetAll();
            return result.ToList();
        }

        public async Task<IList<GameDto>> GetGamesByPublisherId(int id)
        {
            var games = await _unit.PublisherRepository.GetGamesByPublisherId(id);
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
                    PlatformTypes = platforms.Where(x => platformsIds.Contains(x.PlatformTypeId)).ToList(),
                };
            }).ToList();

            return result;
        }
    }
}
