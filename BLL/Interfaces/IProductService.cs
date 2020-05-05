using CrossCuttingFunctionality.FilterModels;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetGamesWithFilters(FilterModel filter);
        Task<List<Product>> GetProductsByManufacturerId(int productId);
        Task<List<Product>> GetProductsByCategoryId(int categoryId);
        Task<int> Create(Product product);
        Task Edit(int productId, Product product);
        Task Delete(int productId);
        Task<Product> GetById(int productId);
        Task<List<Product>> GetAll();
    }
}
