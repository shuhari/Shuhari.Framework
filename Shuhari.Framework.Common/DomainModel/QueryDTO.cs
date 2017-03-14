using System.Data;
using Newtonsoft.Json;
using Shuhari.Framework.Data;
using Shuhari.Framework.Globalization;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.DomainModel
{
    /// <summary>
    /// Base type for query critias
    /// </summary>
    public class QueryDto
    {
        /// <summary>
        /// Default initialize
        /// </summary>
        public QueryDto()
        {
        }

        /// <summary>
        /// Initialize with pagination
        /// </summary>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        public QueryDto(int page, int perPage)
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

            Page = page;
            PerPage = perPage;
        }

        /// <summary>
        /// Name of pagination parameter (Offset)
        /// </summary>
        public const string PARAM_OFFSET = "offset";

        /// <summary>
        /// Name of pagination parameter (Limit)
        /// </summary>
        public const string PARAM_LIMIT = "limit";

        /// <summary>
        /// Set query parameter.
        /// Base class set pagination info only, derived class
        /// can override and set custom parameters.
        /// </summary>
        /// <param name="query"></param>
        public virtual void SetQuery(IQueryBase query)
        {
            Expect.IsNotNull(query, nameof(query));

            query.SetParam(PARAM_OFFSET, DbType.Int32, Offset);
            query.SetParam(PARAM_LIMIT, DbType.Int32, PerPage);
        }

        /// <summary>
        /// Generate critia list. Can be overrided by children
        /// </summary>
        /// <returns></returns>
        public virtual CritiaList ToCritias()
        {
            return new CritiaList();
        }
    }
}
