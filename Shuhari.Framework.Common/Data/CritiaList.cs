using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Data
{
    /// <summary>
    /// Set critias and convert to where ... clause
    /// </summary>
    public class CritiaList
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public CritiaList()
        {
            _critias = new List<string>();
        }

        private List<string> _critias;

        /// <summary>
        /// Add critia
        /// </summary>
        /// <param name="critia"></param>
        /// <returns></returns>
        public CritiaList Add(string critia)
        {
            Expect.IsNotBlank(critia, nameof(critia));

            _critias.Add(critia);
            return this;
        }

        /// <summary>
        /// Add critia only when <paramref name="predicate"/> is true
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="critia"></param>
        /// <returns></returns>
        public CritiaList AddIf(bool predicate, string critia)
        {
            Expect.IsNotBlank(critia, nameof(critia));

            if (predicate)
                _critias.Add(critia);
            return this;
        }

        /// <summary>
        /// Get where clause
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (_critias.Count == 0)
                return string.Empty;
            else
            {
                var critias = _critias.Select(x => string.Format("({0})", x));
                return string.Format("where {0}", string.Join(" and ", critias));
             }
        }
    }
}
