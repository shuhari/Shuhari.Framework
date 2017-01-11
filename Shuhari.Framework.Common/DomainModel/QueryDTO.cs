﻿using Newtonsoft.Json;
using Shuhari.Framework.Globalization;
using Shuhari.Framework.Resources;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.DomainModel
{
    /// <summary>
    /// Base type for query critias
    /// </summary>
    public class QueryDTO
    {
        /// <summary>
        /// Default initialize
        /// </summary>
        public QueryDTO()
        {
        }

        /// <summary>
        /// Initialize with pagination
        /// </summary>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        public QueryDTO(int page, int perPage)
        {
            SetPagination(page, perPage);
        }

        /// <summary>
        /// Current page index, start from 0
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Record count per page
        /// </summary>
        public int PerPage { get; set; }

        /// <summary>
        /// Start offset
        /// </summary>
        [JsonIgnore]
        public int Offset
        {
            get { return Page * PerPage; }
        }

        /// <summary>
        /// Set paginiation info
        /// </summary>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        public void SetPagination(int page, int perPage)
        {
            Expect.That(page >= 0, FrameworkStrings.ErrorNumberShouldBePositive);
            Expect.That(perPage >= 0, FrameworkStrings.ErrorNumberShouldBePositive);

            this.Page = page;
            this.PerPage = perPage;
        }
    }
}
