using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Shuhari.Framework.Utils
{
    /// <summary>
    /// Helper methods to extend ObservableCollection{T}
    /// </summary>
    public static class ObservableCollectionUtil
    {
        /// <summary>
        /// Add multiple items to collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="items"></param>
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            Expect.IsNotNull(collection, nameof(collection));
            Expect.IsNotNull(items, nameof(items));

            foreach (var item in items)
                collection.Add(item);
        }

        /// <summary>
        /// Sort items
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        public static void Sort<T>(this ObservableCollection<T> collection)
            where T: IComparable
        {
            Expect.IsNotNull(collection, nameof(collection));

            var array = collection.ToArray();
            Array.Sort(array);
            collection.Clear();
            collection.AddRange(array);
        }
    }
}
