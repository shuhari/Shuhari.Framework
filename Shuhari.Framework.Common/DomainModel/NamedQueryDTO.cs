using System.Data;
using Shuhari.Framework.Data;

namespace Shuhari.Framework.DomainModel
{
    /// <summary>
    /// Query with only a name critia
    /// </summary>
    public class NamedQueryDTO : QueryDTO
    {
        /// <summary>
        /// Default initialize
        /// </summary>
        public NamedQueryDTO()
            : base()
        {
        }

        /// <summary>
        /// Initialize with pagination and critias
        /// </summary>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="name"></param>
        public NamedQueryDTO(int page, int perPage, string name)
            : base(page, perPage)
        {
            this.Name = name;
        }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// parameter name
        /// </summary>
        public const string PARAM_NAME = "Name";

        /// <inheritdoc />
        public override void SetQuery(IQuery query)
        {
            base.SetQuery(query);

            query.Set(PARAM_NAME, DbType.String, Name);
        }
    }
}
