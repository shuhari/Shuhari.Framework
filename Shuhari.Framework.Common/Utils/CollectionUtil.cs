using System;
using System.Collections.Generic;
using System.Linq;
using Shuhari.Framework.DomainModel;

namespace Shuhari.Framework.Utils
{
    /// <summary>
    /// Helper to process collection/enumerable
    /// </summary>
    public static class CollectionUtil
    {
        /// <summary>
        /// Perform <paramref name="action"/> for each element in <paramref name="collection"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="action"></param>
        public static void Each<T>(this IEnumerable<T> collection, Action<T> action)
        {
            Expect.IsNotNull(collection, nameof(collection));
            Expect.IsNotNull(action, nameof(action));

            foreach (var item in collection)
                action(item);
        }

        /// <summary>
        /// Find in a <paramref name="collection"/> whose property match specified <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProp"></typeparam>
        /// <param name="collection"></param>
        /// <param name="selector"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T FindBy<T, TProp>(this IEnumerable<T> collection, Func<T, TProp> selector, TProp value)
        {
            Expect.IsNotNull(collection, nameof(collection));
            Expect.IsNotNull(selector, nameof(selector));

            return collection.FirstOrDefault(x => object.Equals(selector(x), value));
        }

        /// <summary>
        /// Find object by name, or return null if not found.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="name">Name to find</param>
        /// <param name="ignoreCase">Use case-ignored string comparasion if set</param>
        /// <returns></returns>
        public static T FindByName<T>(this IEnumerable<T> collection, string name, bool ignoreCase = false)
            where T : INamed
        {
            Expect.IsNotNull(collection, nameof(collection));

            StringComparison flags = ignoreCase ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture;
            return collection.FirstOrDefault(x => string.Equals(x.Name, name, flags));
        }

        /// <summary>
        /// Find <paramref name="collection"/> by string property, optionally ignoring case
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="selector"></param>
        /// <param name="value"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static T FindBy<T>(this IEnumerable<T> collection, Func<T, string> selector, 
            string value, bool ignoreCase)
        {
            Expect.IsNotNull(collection, nameof(collection));
            Expect.IsNotNull(selector, nameof(selector));

            var flag = ignoreCase ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture;
            return collection.FirstOrDefault(x => string.Equals(selector(x), value, flag));
        }
    }
}
