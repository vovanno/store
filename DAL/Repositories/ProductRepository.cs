using DAL.Exceptions;
using DAL.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Context;
using Domain.Entities.FilterModels;

namespace DAL.Repositories
{
    internal sealed class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProductsByManufacturerId(int manufacturerId)
        {
            var games = await _context.Products.AsNoTracking().Where(p => p.ManufacturerId == manufacturerId)
                .GetAllProductProperties()
                .ToListAsync();
            return games;
        }

        public async Task<List<Product>> GetProductsByIds(int[] productsIds)
        {
            return await _context.Products
                .GetAllProductProperties()
                .Where(p => productsIds.Contains(p.ProductId)).ToListAsync();
        }

        public async Task<List<Product>> ProductsWithCategoryId(int categoryId)
        {
            var games = await _context.Products.AsNoTracking().Where(p => p.CategoryId == categoryId)
                .GetAllProductProperties()
                .ToListAsync();

            return games;
        }

        public async Task<Product> CheckById(int productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productId);

            return product ?? throw new EntryNotFoundException($"Product with ProductId={productId} was not found");
        }

        public async Task<Product> GetById(int productId)
        {
            var product = await _context.Products
                .Include(p => p.Manufacturer)
                .Include(p => p.Images)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == productId);

            return product ?? throw new EntryNotFoundException($"Product with ProductId={productId} was not found");
        }

        public async Task<Product> FindById(int productId)
        {
            var game = await _context.Products
                .GetAllProductProperties()
                .FirstOrDefaultAsync(p => p.ProductId == productId);

            return game;
        }

        public async Task<List<Product>> GetAll()
        {
            var result =  await _context.Products.AsNoTracking()
                .GetAllProductProperties()
                .ToListAsync();
            return result;
        }

        public async Task<Product> Add(Product game)
        {
            var gameEntity = await _context.Products.AddAsync(game);

            return gameEntity.Entity;
        }

        public async Task UploadImages(int productId, List<string> imagesNames)
        {
            var images = imagesNames.Select(p => new Image()
            {
                ImageKey = p,
                ProductId = productId
            });
            await _context.Images.AddRangeAsync(images);
        }

        public async Task UploadImage(int productId, string imageName, bool isMain)
        {
            var image = new Image()
            {
                ImageKey = imageName,
                ProductId = productId,
                IsMain = isMain
            };
            await _context.Images.AddRangeAsync(image);
        }

        public async Task Update(int productId, Product updatedGame)
        {
            var modifiedObj = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
            updatedGame.ProductId = productId;

            _context.Entry(modifiedObj).CurrentValues.SetValues(updatedGame);
        }

        public async Task Delete(int productId)
        {
            await _context.Database.ExecuteSqlCommandAsync("DELETE FROM Products WHERE ProductId=@productId",
                new MySqlParameter("@productId", productId));
        }

        public async Task<List<Category>> GetGameGenres(int productId)
        {
            var result = await _context.Categories.FromSql(
                "SELECT ge.CategoryId, ge.Name FROM Categories ge INNER JOIN Categories gg ON ge.CategoryId = gg.CategoryId WHERE gg.ProductId = @productId",
                new MySqlParameter("@productId", productId)).ToListAsync();
            return result;
        }

        public async Task LeaveComment(int productId, Comment comment)
        {
            await _context.Comments.AddAsync(comment);
        }

        public async Task<List<Product>> GetProductsWithFilters(FilterModel filter)
        {
            IOrderedEnumerable<Product> filteredResult = null; 

            var query = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Manufacturer)
                .Include(p => p.Images)
                .Where(p => p.CategoryId == filter.CategoryId &&
                            p.Price > filter.PriceFilter.From && p.Price <= filter.PriceFilter.To);

            if (filter.ManufacturersIds.Length > 0)
                query = query.Where(p => filter.ManufacturersIds.Contains(p.Manufacturer.ManufacturerId));

            if (filter.IsMostPopular)
                query.Include(p => p.Comments);
            var result = await query.ToListAsync();

            if (filter.IsMostCommented)
            {
                filteredResult = result.OrderBy(p => p.Comments.Count);
            }
            else if (filter.IsMostPopular)
            {
                filteredResult = result.OrderBy(p => p.Comments?.Count);
            }
            else if (filter.ByPriceAscending)
            {
                filteredResult = result.OrderBy(p => p.Price);
            }
            else if (filter.ByPriceDescending)
            {
                filteredResult = result.OrderByDescending(p => p.Price);
            }

            return filteredResult?.Skip((filter.Page -1) * filter.Size).Take(filter.Size).ToList();
        }
    }
}
