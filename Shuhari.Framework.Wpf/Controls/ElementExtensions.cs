using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Wpf.Controls
{
    /// <summary>
    /// Extension methods for UIElement/FrameworkElement
    /// </summary>
    public static class ElementExtensions
    {
        /// <summary>
        /// Enable/disable all elements
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="enable"></param>
        /// <param name="elems"></param>
        public static void EnableElements(this UIElement parent, bool enable, params UIElement[] elems)
        {
            Expect.IsNotNull(parent, nameof(parent));
            Expect.IsNotNull(elems, nameof(elems));

            foreach (var elem in elems)
                elem.IsEnabled = enable;
        }

        /// <summary>
        /// 当用户按回车及点击按钮时均触发默认动作
        /// </summary>
        /// <param name="elem"></param>
        /// <param name="triggerBtn"></param>
        /// <param name="action"></param>
        public static void RegisterDefaultAction(this UIElement elem, Button triggerBtn, Action action)
        {
            Expect.IsNotNull(elem, nameof(elem));
            Expect.IsNotNull(action, nameof(action));

            elem.PreviewKeyUp += (sender, e) =>
            {
                if (e.Key == Key.Enter)
                    action();
            };

            if (triggerBtn != null)
                triggerBtn.Click += (sender, e) => action();
        }

        /// <summary>
        /// Show element
        /// </summary>
        /// <param name="elem"></param>
        public static void Show(this UIElement elem)
        {
            Expect.IsNotNull(elem, nameof(elem));

            elem.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Hide element
        /// </summary>
        /// <param name="elem"></param>
        public static void Hide(this UIElement elem)
        {
            Expect.IsNotNull(elem, nameof(elem));

            elem.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Set dependency property if not locally set
        /// </summary>
        /// <param name="elem"></param>
        /// <param name="prop"></param>
        /// <param name="value"></param>
        public static void SetUserProperty(this FrameworkElement elem, DependencyProperty prop, object value)
        {
            Expect.IsNotNull(prop, nameof(prop));

            if (elem != null && elem.ReadLocalValue(prop) == DependencyProperty.UnsetValue)
                elem.SetValue(prop, value);
        }

        /// <summary>
        /// Find matched parent throught visual tree until <paramref name="predicate"/> satisfied or no parent
        /// </summary>
        /// <param name="elem"></param>
        /// <param name="predicate"></param>
        /// <returns>Matched parent </returns>
        public static DependencyObject FindAncestor(this DependencyObject elem, Predicate<DependencyObject> predicate)
        {
            Expect.IsNotNull(predicate, nameof(predicate));

            if (elem == null)
                return null;
            if (predicate(elem))
                return elem;
            return FindAncestor(VisualTreeHelper.GetParent(elem), predicate);
        }

        /// <summary>
        /// Find ancestor for specified type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="elem"></param>
        /// <returns></returns>
        public static T FindAncestor<T>(this DependencyObject elem)
            where T: FrameworkElement
        {
            return (T) FindAncestor(elem, x => x is T);
        }
    }
}
