using DAL.Interfaces;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Exceptions;
using Domain;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace DAL.Repositories
{
    internal sealed class PublisherRepository : IPublisherRepository
    {
        private readonly AppContext _context;

        public PublisherRepository(AppContext context)
        {
            _context = context;
        }

        public async Task<IList<Game>> GetGamesByPublisherId(int publisherId)
        {
            var games = await _context.Games.AsNoTracking().Where(p => p.PublisherId == publisherId)
                .Include(p => p.GameGenres)
                .Include(p => p.PlatformTypes)
                .Include(p => p.Comments)
                .ToListAsync();
            return games;
        }

        public async Task<Publisher> GetById(int publisherId)
        {
            return await _context.Publishers.FirstOrDefaultAsync(p => p.PublisherId == publisherId);
        }

        public async Task<IList<Publisher>> GetAll()
        {
            return await _context.Publishers.AsNoTracking().ToListAsync();
        }

        public async Task<Publisher> Add(Publisher publisher)
        {
            var createdPublished = await _context.Publishers.AddAsync(publisher);
            return createdPublished.Entity;
        }

        public async Task Update(int publisherId, Publisher updatedEntity)
        {
            var foundPublisher = await _context.Publishers.FirstOrDefaultAsync(p => p.PublisherId == publisherId);

            if (foundPublisher == null)
                throw new EntryNotFoundException($"Publisher with PublisherId={publisherId} was not found");

            _context.Entry(foundPublisher).CurrentValues.SetValues(updatedEntity.ToSqlUpdateParameters());
        }

        public async Task Delete(int publisherId)
        {
            await _context.Database.ExecuteSqlCommandAsync("DELETE FROM Publishers WHERE PublisherId=@publisherId",
                new MySqlParameter("@publisherId", publisherId));
        }
    }
}
