using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
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

        /// <inheritdoc />
        [JsonIgnore]
        public int TotalPages
        {
            get { return (Total + PerPage - 1) / PerPage; }
        }

        /// <summary>
        /// Set pagination info
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public PagedCollection<T> SetPagination(QueryDto q)
        {
            Expect.IsNotNull(q, nameof(q));

            Page = q.Page;
            PerPage = q.PerPage;

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

        private const int PAGINATION_EXPAND = 2;

        /// <inheritdoc />
        public Pager CalculatePager()
        {
            Expect.That(PerPage > 0, string.Format("PerPage<={0} could not calculate pagination", PerPage));

            var pager = new Pager();
            pager.Total = Total;
            pager.StartIndex = Page * PerPage;
            pager.EndIndex = Math.Max(0, Math.Min((Page + 1) * PerPage - 1, Total - 1));

            if (Total > 0)
            {
                var items = new List<PagerItem>();

                int prevTo = Math.Max(0, Page - PAGINATION_EXPAND);
                int firstTo = Math.Min(PAGINATION_EXPAND, prevTo - 1);
                for (int pageNo = 0; pageNo <= firstTo; pageNo++)
                    items.Add(CreatePaginationItem(pageNo, null, false));
                if (firstTo >= 0 && firstTo < prevTo - 1)
                    items.Add(CreatePaginationItem(-1, "...", true));
                for (int pageNo = prevTo; pageNo < Page; pageNo++)
                    items.Add(CreatePaginationItem(pageNo, null, false));

                items.Add(CreatePaginationItem(Page, null, true));

                var nextTo = Math.Min(Page + PAGINATION_EXPAND, TotalPages - 1);
                var lastTo = Math.Max(nextTo + 1, TotalPages - 1 - PAGINATION_EXPAND);
                for (int pageNo = Page + 1; pageNo <= nextTo; pageNo++)
                    items.Add(CreatePaginationItem(pageNo, null, false));
                if (nextTo < lastTo - 1)
                    items.Add(CreatePaginationItem(-1, "...", true));
                for (int pageNo = lastTo; pageNo <= TotalPages - 1; pageNo++)
                    items.Add(CreatePaginationItem(pageNo, null, false));

                pager.Items = items.ToArray();
            }

            return pager;
        }

        private PagerItem CreatePaginationItem(int pageNo, string displayName, bool disabled)
        {
            displayName = displayName ?? string.Format("{0}", pageNo + 1);
            bool isCurrent = (pageNo == this.Page);
            if (pageNo < 0 || isCurrent)
                disabled = true;
            return new PagerItem(pageNo, displayName, isCurrent, disabled);
        }
    }
}
