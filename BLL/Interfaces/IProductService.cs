using Domain.Entities;
using OnlineStoreApi.ProductApi;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities.FilterModels;

namespace BLL.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsWithFilters(FilterModel filter);
        Task<List<Product>> GetProductsByManufacturerId(int productId);
        Task<List<Product>> GetProductsByCategoryId(int categoryId);
        Task<int> Create(Product product);
        Task Edit(int productId, Product product);
        Task Delete(int productId);
        Task<Product> GetById(int productId);
        Task<List<Product>> GetAll();
        Task UploadImages(int productId, CreateImageRequest[] createImagesRequest);
        Task<List<Product>> GetProductsByIds(int[] productsIds);
    }
}
