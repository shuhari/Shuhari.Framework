using System;
using System.Collections.Generic;
using System.Linq;
using Shuhari.Framework.Globalization;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.DomainModel
{
    /// <summary>
    /// Save collection data with pagination info for query result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedCollection<T> : IPagination
    {
        /// <inheritdoc />
        public int Page { get; set; }

        /// <inheritdoc />
        public int PerPage { get; set; }

        /// <inheritdoc />
        public int Total { get; set; }

        /// <summary>
        /// Data collection
        /// </summary>
        public T[] Data { get; set; }

        /// <summary>
        /// Set pagination info
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public PagedCollection<T> SetPagination(QueryDTO q)
        {
            Expect.IsNotNull(q, nameof(q));

            this.Page = q.Page;
            this.PerPage = q.PerPage;

            return this;
        }

        /// <summary>
        /// Set data
        /// </summary>
        /// <param name="total"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public PagedCollection<T> SetData(int total, IEnumerable<T> data)
        {
            Expect.That(total >= 0, FrameworkStrings.ErrorNumberShouldBePositive);
            Expect.IsNotNull(data, nameof(data));

            this.Total = total;
            this.Data = data.ToArray();

            return this;
        }

        /// <summary>
        /// Map to a new collection
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public PagedCollection<TResult> MapTo<TResult>(Func<T, TResult> mapper)
        {
            Expect.IsNotNull(mapper, nameof(mapper));

            var result = new PagedCollection<TResult>();
            result.Page = Page;
            result.PerPage = PerPage;
            result.SetData(Total, Data.Select(mapper));

            return result;
        }
    }
}
