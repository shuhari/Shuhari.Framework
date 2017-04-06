using NUnit.Framework;
using Shuhari.Framework.ComponentModel;

namespace Shuhari.Framework.UnitTests.ComponentModel
{
    [TestFixture]
    public class SelectableTest
    {
        class TestEntity : Selectable
        {
        }

        [Test]
        public void Get_SetSelected()
        {
            var changed = false;
            var entity = new TestEntity();
            entity.PropertyChanged += (sender, e) => { if (e.PropertyName == "Selected") changed = true; };
            entity.Selected = true;

            Assert.IsTrue(entity.Selected);
            Assert.IsTrue(changed);
        }
    }
}
