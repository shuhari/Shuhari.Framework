using System.Windows;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Wpf.Controls
{
    /// <summary>
    /// Base class for dialog
    /// </summary>
    public class Dialog : Window
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public Dialog()
        {
            SizeToContent = SizeToContent.WidthAndHeight;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            ShowInTaskbar = false;

            var mainWin = Application.Current.MainWindow;
            if (mainWin != null)
            {
                FontFamily = mainWin.FontFamily;
                FontSize = mainWin.FontSize;
            }
        }

        /// <summary>
        /// Set owner before show dialog 
        /// </summary>
        /// <param name="owner">Owner window</param>
        /// <returns></returns>
        public bool? ShowDialog(Window owner)
        {
            Expect.IsNotNull(owner, nameof(owner));

            this.Owner = owner;
            return base.ShowDialog();
        }

        /// <summary>
        /// Set dialog result before close
        /// </summary>
        /// <param name="result">Dialog result</param>
        protected void Close(bool? result)
        {
            DialogResult = result;
            Close();
        }
    }
}
