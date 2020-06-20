using Amazon.S3;
using Amazon.S3.Model;
using BLL.Interfaces;
using DAL.Interfaces;
using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.FilterModels;
using OnlineStoreApi.ProductApi;

namespace BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unit;
        private readonly IAmazonS3 _s3Client;

        public ProductService(IUnitOfWork unit, IAmazonS3 s3Client)
        {
            _unit = unit;
            _s3Client = s3Client;
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

        public async Task<List<Product>> GetProductsByIds(int[] ids)
        {
            var products = await _unit.ProductRepository.GetProductsByIds(ids);
            products.CountRatingForProducts();

            return products;
        }

        public async Task<List<Product>> GetAll()
        {
            var products = await _unit.ProductRepository.GetAll();
            products.CountRatingForProducts();

            return products;
        }

        public async Task<List<Product>> GetProductsWithFilters(FilterModel filter)
        {
            if (filter.PriceFilter.To == 0)
                filter.PriceFilter.To = int.MaxValue;
            var products = await _unit.ProductRepository.GetProductsWithFilters(filter);
            products.CountRatingForProducts();

            return products;
        }

        public async Task<List<Product>> GetProductsByManufacturerId(int manufacturerId)
        {
            var products = await _unit.ProductRepository.GetProductsByManufacturerId(manufacturerId);
            products.CountRatingForProducts();

            return products;
        }

        public async Task UploadImages(int productId, CreateImageRequest[] createImagesRequest)
        {
            var bucketName = "computer-store-images";

            await _unit.ProductRepository.GetById(productId);

            foreach (var imageRequest in createImagesRequest.OrderBy(p => p.IsMain))
            {
                var imageName = Guid.NewGuid().ToString("N");
                var base64Content = imageRequest.ImageContent.Split(',')[1];
                var buffer = Convert.FromBase64String(base64Content);
                var stream = new MemoryStream(buffer);

                try
                {
                    await _s3Client.PutObjectAsync(new PutObjectRequest
                    {
                        Key = imageName,
                        BucketName = bucketName,
                        InputStream = stream
                    });
                }
                catch (Exception)
                {
                    if(imageRequest.IsMain)
                        throw new Exception("Error occured during uploading of main image, skipping other uploads.");
                    continue;
                }

                await _unit.ProductRepository.UploadImage(productId, imageName, imageRequest.IsMain);
                await _unit.Commit();
            }
        }
    }
}
