using System;
using System.Collections.Generic;
using System.Linq;

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

                if(argumentValue == null)
                    continue;

                var argumentType = argumentValue.GetType();
                var defaultValue = argumentType.IsValueType ? Activator.CreateInstance(argumentType) : null;

                if (argumentValue == defaultValue)
                    continue;

                sqlParameters.Add(prop.Name, argumentValue);
            }

            return sqlParameters.Any() ? sqlParameters : null;
        }
    }
}
