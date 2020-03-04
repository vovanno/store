using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Exceptions;
using DAL.Interfaces;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace DAL.Repositories
{
    internal sealed class PlatformTypeRepository : IPlatformRepository
    {
        private readonly AppContext _context;

        public PlatformTypeRepository(AppContext context)
        {
            _context = context;
        }

        public async Task<PlatformType> GetById(int platformId)
        {
            return await _context.PlatformTypes.FirstOrDefaultAsync(p => p.PlatformTypeId == platformId);
        }

        public async Task<IList<PlatformType>> GetAll()
        {
            return await _context.PlatformTypes.AsNoTracking().ToListAsync();
        }

        public async Task<PlatformType> Add(PlatformType entity)
        {
            var platform = await _context.PlatformTypes.AddAsync(entity);

            return platform.Entity;
        }

        public async Task Update(int platformId, PlatformType updatedEntity)
        {
            var foundPlatform = await _context.PlatformTypes.FirstOrDefaultAsync(p => p.PlatformTypeId == platformId);
            if(foundPlatform == null)
                throw new EntryNotFoundException($"Platform with PlatformId={platformId} not found");

            _context.Entry(foundPlatform).CurrentValues.SetValues(updatedEntity.ToSqlUpdateParameters());
        }

        public async Task Delete(int platformId)
        {
            await _context.Database.ExecuteSqlCommandAsync("DELETE FROM PlatformTypes WHERE PlatformTypeId=@platformId",
                new MySqlParameter("@platformId", platformId));
        }
    }
}
