using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Shuhari.Framework.Web
{
    /// <summary>
    /// Mock session
    /// </summary>
    internal class MockHttpSession : HttpSessionStateBase
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public MockHttpSession()
        {
            _items = new Dictionary<string, object>();
        }

        private Dictionary<string, object> _items;

        /// <inheritdoc />
        public override object this[string name]
        {
            get { return _items.ContainsKey(name) ? _items[name] : null; }
            set { _items[name] = value; }
        }

        /// <inheritdoc />
        public override void Remove(string name)
        {
            _items.Remove(name);
        }
    }
}
