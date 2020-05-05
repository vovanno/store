using BLL.Interfaces;
using CrossCuttingFunctionality.FilterModels;
using DAL.Interfaces;
using Domain;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unit;

        public ProductService(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<List<Product>> GetProductsByCategoryId(int categoryId)
        {
            var products = await _unit.ProductRepository.ProductsWithCategoryId(categoryId);
            products.CountRatingForProducts();

            return products;
        }

        public async Task<int> Create(Product product)
        {
            var createdProduct = await _unit.ProductRepository.Add(product);
            await _unit.Commit();
            return createdProduct.ProductId;
        }

        public async Task Edit(int productId, Product game)
        {
            await _unit.ProductRepository.Update(productId, game);
            await _unit.Commit();
        }

        public async Task Delete(int productId)
        {
            await _unit.ProductRepository.Delete(productId);
            await _unit.Commit();
        }

        public async Task<Product> GetById(int productId)
        {
            var product = await _unit.ProductRepository.GetById(productId);
            product.CountRatingForProduct();
            
            return product;
        }

        public async Task<List<Product>> GetAll()
        {
            var products = await _unit.ProductRepository.GetAll();
            products.CountRatingForProducts();

            return products;
        }

        public async Task<List<Product>> GetGamesWithFilters(FilterModel filter)
        {
            var products = await _unit.ProductRepository.GetGamesWithFilters(filter);
            products.CountRatingForProducts();

            return products;
        }

        public async Task<List<Product>> GetProductsByManufacturerId(int manufacturerId)
        {
            var products = await _unit.ProductRepository.GetProductsByManufacturerId(manufacturerId);
            products.CountRatingForProducts();

            return products;
        }
    }
}
