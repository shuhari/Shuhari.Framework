using System;
using System.Linq.Expressions;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Data
{
    /// <summary>
    /// Describe order by specified property
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class OrderCritia<T>
        where T: class
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="ascending"></param>
        /// <param name="value"></param>
        public OrderCritia(Expression<Func<T, object>> selector, object value, bool ascending = true)
        {
            Expect.IsNotNull(selector, nameof(selector));

            this.Selector = selector;
            this.Value = value;
            this.Ascending = ascending;
        }

        /// <summary>
        /// Property selector
        /// </summary>
        public Expression<Func<T, object>> Selector { get; private set; }

        /// <summary>
        /// Property value
        /// </summary>
        public object Value { get; private set; }

        /// <summary>
        /// Ascending/descending
        /// </summary>
        public bool Ascending { get; private set; }
    }
}
