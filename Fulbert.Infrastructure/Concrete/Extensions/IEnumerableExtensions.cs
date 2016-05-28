using System;
using System.Collections.Generic;
using System.Linq;

namespace Fulbert.Infrastructure.Concrete.Extensions
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Code from: http://stackoverflow.com/questions/14266753/search-a-list-of-objects-for-any-and-all-matches
        /// </summary>
        public static IEnumerable<T> WhereAtLeastOneProperty<T, PropertyType>(this IEnumerable<T> source, Predicate<PropertyType> predicate)
        {
            var properties = typeof(T).GetProperties().Where(prop => prop.CanRead && prop.PropertyType == typeof(PropertyType)).ToArray();
            return source.Where(item => properties.Any(prop => PropertySatisfiesPredicate(predicate, item, prop)));
        }

        private static bool PropertySatisfiesPredicate<T, PropertyType>(Predicate<PropertyType> predicate, T item, System.Reflection.PropertyInfo prop)
        {
            try
            {
                return predicate((PropertyType)prop.GetValue(item));
            }
            catch
            {
                return false;
            }
        }
    }
}
