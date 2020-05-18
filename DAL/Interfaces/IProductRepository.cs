using CrossCuttingFunctionality.FilterModels;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IProductRepository
    {
        Task LeaveComment(int productId, Comment comment);
        Task<List<Product>> GetGamesWithFilters(FilterModel filter);
        Task<List<Product>> ProductsWithCategoryId(int categoryId);
        Task<List<Product>> GetProductsByManufacturerId(int publisherId);
        Task<List<Product>> GetProductsByIds(int[] productsIds);
        Task<Product> GetById(int productId);
        Task<Product> FindById(int productId);
        Task<List<Product>> GetAll();
        Task<Product> Add(Product product);
        Task Update(int productId, Product updatedEntity);
        Task Delete(int productId);
        Task UploadImages(int productId, List<string> imagesNames);
        Task UploadImage(int productId, string imagesName, bool isMain);
    }
}
