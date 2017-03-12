using System;
using System.Collections.Generic;

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
        /// Find first matched elements
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="predicate"></param>
        public static T Find<T>(this IEnumerable<T> collection, Predicate<T> predicate)
            where T: class
        {
            Expect.IsNotNull(collection, nameof(collection));
            Expect.IsNotNull(predicate, nameof(predicate));

            foreach (var item in collection)
                if (predicate(item))
                    return item;

            return null;
        }

        /// <summary>
        /// Find index of first matched element, or -1 if not found.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static int FindIndex<T>(this IEnumerable<T> collection, Predicate<T> predicate)
        {
            Expect.IsNotNull(collection, nameof(collection));
            Expect.IsNotNull(predicate, nameof(predicate));

            int index = 0;
            foreach (var item in collection)
            {
                if (predicate(item))
                    return index;
                else
                    index++;
            }

            return -1;
        }

        /// <summary>
        /// Return empty collection if <see paramref="collection"/> is null,
        /// to avoid NPE exception
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static IEnumerable<T> Safe<T>(this IEnumerable<T> collection)
        {
            return collection ?? new T[0];
        }
    }
}
