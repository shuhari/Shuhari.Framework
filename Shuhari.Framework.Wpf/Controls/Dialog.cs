using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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

            Loaded += Dialog_Loaded;
        }

        /// <summary>
        /// Get role
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static ElementRole GetRole(DependencyObject obj)
        {
            return (ElementRole)obj.GetValue(RoleProperty);
        }

        /// <summary>
        /// Set role
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetRole(DependencyObject obj, ElementRole value)
        {
            obj.SetValue(RoleProperty, value);
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for Role.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty RoleProperty =
            DependencyProperty.RegisterAttached("Role", 
                typeof(ElementRole), typeof(Dialog), 
                new PropertyMetadata(ElementRole.None));


        private void Dialog_Loaded(object sender, RoutedEventArgs e)
        {
            WalkChildrenToApplyRoles(this);
        }

        private void WalkChildrenToApplyRoles(FrameworkElement target)
        {
            if (target == null)
                return;

            var role = GetRole(target);
            if (target is Button && role == ElementRole.Cancel)
            {
                var btn = (Button)target;
                btn.Click += (sender, e) => { Close(false); };
            }

            var childCount = VisualTreeHelper.GetChildrenCount(target);
            for (int i=0; i<childCount; i++)
            {
                var child = VisualTreeHelper.GetChild(target, i);
                WalkChildrenToApplyRoles(child as FrameworkElement);
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

            Owner = owner;
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
