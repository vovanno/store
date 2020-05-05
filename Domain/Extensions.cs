using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;

namespace Domain
{
    public static class Extensions
    {
        public static IList<T> EmptyIfNull<T>(this IList<T> source)
        {
            return source ?? new List<T>();
        }

        public static Dictionary<string, object> ToSqlUpdateParameters<T>(this T source)
        {
            var sqlParameters = new Dictionary<string, object>();

            var type = source.GetType();
            var props = type.GetProperties();

            foreach (var prop in props)
            {
                dynamic argumentValue = prop.GetValue(source);

                if (argumentValue == null)
                    continue;

                var argumentType = argumentValue.GetType();
                var defaultValue = argumentType.IsValueType ? Activator.CreateInstance(argumentType) : null;

                if (argumentValue == defaultValue)
                    continue;

                sqlParameters.Add(prop.Name, argumentValue);
            }

            return sqlParameters.Any() ? sqlParameters : null;
        }

        public static void CountRatingForProduct(this Product product)
        {
            if (product.Comments == null || product.Comments.Count == 0)
            {
                product.Rating = 0;
                return;
            }

            product.Rating = product.Comments
                .Where(x => x.ParentCommentId == 0 )
                .Select(x => x.ProductRating).Average();
        }

        public static void CountRatingForProducts(this List<Product> products)
        {
            products.ForEach(CountRatingForProduct);
        }
    }
}
