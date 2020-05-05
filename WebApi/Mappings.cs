using Domain;
using Domain.Entities;
using OnlineStoreApi.AuthApi;
using OnlineStoreApi.CommentApi;
using System.Collections.Generic;
using System.Linq;
using OnlineStoreApi.CategoryApi;
using OnlineStoreApi.ManufactureApi;
using OnlineStoreApi.OrdersApi;
using OnlineStoreApi.ProductApi;

namespace WebApi
{
    public static class Mappings
    {
        public static CommentResponse ToCommentResponse(this Comment comment)
        {
            return new CommentResponse
            {
                ProductId = comment.ProductId,
                CommentId = comment.CommentId,
                ProductRating = comment.ProductRating,
                DateOfAdding = comment.DateOfAdding,
                AmountOfLikes = comment.AmountOfLikes,
                Body = comment.Body,
                ParentId = comment.ParentCommentId
            };
        }

        public static List<CommentResponse> ToCommentResponse(this IList<Comment> comments)
        {
            return comments.EmptyIfNull().Select(p => new CommentResponse
            {
                AmountOfLikes = p.AmountOfLikes,
                Body = p.Body,
                ProductRating = p.ProductRating,
                DateOfAdding = p.DateOfAdding,
                CommentId = p.CommentId,
                ParentId = p.CommentId,
                ProductId = p.ProductId
            }).ToList();
        }

        public static Comment ToCommentModel(this CreateCommentRequest request)
        {
            return new Comment
            {
                Body = request.Body,
                ProductRating = request.ProductRating,
                ProductId = request.ProductId,
                ParentCommentId = request.ParentId
            };
        }

        public static Comment ToCommentModel(this EditCommentRequest request)
        {
            return new Comment
            {
                Body = request.Body
            };
        }

        public static UserProfile ToUserProfile(this RegisterUserRequest request)
        {
            return new UserProfile
            {
                Email = request.Email,
                UserName = request.UserName
            };
        }

        public static UserProfile ToUserProfile(this LoginUserRequest request)
        {
            return new UserProfile
            {
                Email = request.Email,
                UserName = request.UserName
            };
        }

        public static ProductResponse ToProductResponse(this Product product)
        {
            return new ProductResponse
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Availability = product.Availability,
                Rating = product.Rating,
                Category = product.Category.ToCategoryResponse(),
                Manufacturer = product.Manufacturer.ToManufacturerResponse(),
                Comments = product.Comments.ToCommentResponse()
            };
        }

        public static ManufacturerResponse ToManufacturerResponse(this Manufacturer publisher)
        {
            return new ManufacturerResponse
            {
                Name = publisher.Name,
                ManufacturerId = publisher.ManufacturerId
            };
        }

        public static Product ToProductModel(this CreateProductRequest request)
        {
            return new Product
            {
                Description = request.Description,
                Name = request.Name,
                Availability = request.Availability,
                ManufacturerId = request.ManufacturerId,
                Price = request.Price,
                CategoryId = request.CategoryId,
            };
        }

        public static Product ToProductModel(this EditProductRequest request)
        {
            return new Product
            {
                Description = request.Description,
                Name = request.Name,
                Availability = request.Availability,
                ManufacturerId = request.ManufacturerId,
                Price = request.Price,
                CategoryId = request.CategoryId,
            };
        }

        public static CategoryResponse ToCategoryResponse(this Category category)
        {
            return new CategoryResponse
            {
                Name = category.Name,
                CategoryId = category.CategoryId
            };
        }

        public static List<CategoryResponse> ToCategoryResponse(this IList<Category> gameGenre)
        {
            return gameGenre.EmptyIfNull().Select(p => new CategoryResponse
            {
                Name = p.Name,
                CategoryId = p.CategoryId
            }).ToList();
        }

        public static Category ToCategoryModel(this CreateCategoryRequest request)
        {
            return new Category
            {
                Name = request.Name
            };
        }

        public static Category ToCategoryModel(this EditCategoryRequest request)
        {
            return new Category
            {
                Name = request.Name
            };
        }

        public static Manufacturer ToManufacturerModel(this CreateManufacturerRequest request)
        {
            return new Manufacturer
            {
                Name = request.Name
            };
        }

        public static Manufacturer ToManufacturerModel(this EditManufacturerRequest request)
        {
            return new Manufacturer
            {
                Name = request.Name
            };
        }

        public static OrderResponse ToOrderResponse(this Order order)
        {
            return new OrderResponse
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                Status = order.Status,
                OrderedProducts = order.Products.Select(p => p.Product.ToProductResponse()).ToList()
            };
        }

        public static List<OrderResponse> ToOrderResponse(this IList<Order> orders)
        {
            return orders.Select(p => p.ToOrderResponse()).ToList();
        }

    }
}
