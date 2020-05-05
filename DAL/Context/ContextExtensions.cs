using System.Linq;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
    public static class ContextExtensions
    {
        public static IQueryable<Product> GetAllProductProperties(this IQueryable<Product> query)
        {
            return query.Include(p => p.Images)
                .Include(p => p.Category)
                .Include(p => p.Manufacturer)
                .Include(p => p.Comments);
        }

        public static IQueryable<Product> GetAllProductProperties(this DbSet<Product> product)
        {
            return product.Include(p => p.Images)
                .Include(p => p.Category)
                .Include(p => p.Manufacturer)
                .Include(p => p.Comments);
        }
    }
}
