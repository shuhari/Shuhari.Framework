using System.Windows;
using Shuhari.Framework.Wpf.Controls;

namespace Shuhari.Framework.Wpf.GuiTests
{
    /// <summary>
    /// Interaction logic for TestDialog.xaml
    /// </summary>
    public partial class TestDialog : Dialog
    {
        public TestDialog()
        {
            InitializeComponent();
        }

        protected override void OnOk()
        {
            MessageBox.Show("OK triggered");
        }
    }
}
