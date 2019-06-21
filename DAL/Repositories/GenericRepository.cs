using DAL.Exceptions;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    internal class GenericRepository<T> : IBaseRepository<T> where T : class, ISoftDelete
    {
        protected readonly DbSet<T> Db;
        protected readonly IAppContext Context;

        public GenericRepository(IAppContext context)
        {
            Context = context;
            Db = context.Set<T>();
        }
        public virtual async Task<T> Add(T entity)
        {
            var result = await Db.AddAsync(entity);
            return result.Entity;
        }

        public virtual async Task Delete(int id)
        {
            var deletedObj = await Db.FindAsync(id);
            if (deletedObj == null)
                return;
            deletedObj.IsDeleted = true;
            Db.Update(deletedObj);
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await Db.ToListAsync();
        }

        public virtual async Task<T> GetById(int id)
        {
            return await Db.FindAsync(id);
        }

        public virtual async Task Update(int id, T updatedT)
        {
            var modifiedObj = await Db.FindAsync(id);
            if (modifiedObj == null)
                throw new EntryNotFoundException("Entry with the given key doesn't exist or was deleted earlier.");
            Context.Entry(modifiedObj).CurrentValues.SetValues(updatedT);
        }
    }
}
