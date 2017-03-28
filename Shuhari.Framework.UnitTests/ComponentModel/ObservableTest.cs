using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Shuhari.Framework.ComponentModel;

namespace Shuhari.Framework.UnitTests.ComponentModel
{
    [TestFixture]
    public class ObservableTest
    {
        class ObservableEntity : Observable
        {
            private string _name;

            public string Name
            {
                get { return _name; }
                set { SetProperty(nameof(Name), ref _name, value); }
            }
        }

        [Test]
        public void SetName()
        {
            string changedProp = null;
            var entity = new ObservableEntity();
            entity.PropertyChanged += (sender, e) => changedProp = e.PropertyName;
            entity.Name = "NewName";

            Assert.AreEqual("NewName", entity.Name);
            Assert.AreEqual("Name", changedProp);
        }
    }
}
