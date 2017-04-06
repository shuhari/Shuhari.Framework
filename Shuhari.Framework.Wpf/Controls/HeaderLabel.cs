using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Shuhari.Framework.Wpf.Controls
{
    /// <summary>
    /// Show header
    /// </summary>
    [TemplatePart(Name = "Part_Title", Type = typeof(TextBlock))]
    public class HeaderLabel : Control
    {
        static HeaderLabel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HeaderLabel),
                new FrameworkPropertyMetadata(typeof(HeaderLabel)));

            _defaultBackground = SystemColors.ActiveCaptionBrush;
            _defaultForeground = SystemColors.ActiveCaptionTextBrush;
        }

        private static readonly Brush _defaultBackground;
        private static readonly Brush _defaultForeground;

        /// <summary>
        /// Initialize
        /// </summary>
        public HeaderLabel()
        {
            Background = _defaultBackground;
            Foreground = _defaultForeground;
        }

        private TextBlock _txtTitle;

        /// <inheritdoc />
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _txtTitle = Template.FindName("Part_Title", this) as TextBlock;
            SetTitle(Title);
        }

        /// <summary>
        /// Title
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary>
        /// Title property
        /// </summary>
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title),
                typeof(string), typeof(HeaderLabel),
                new PropertyMetadata("", OnTitleChanged));

        private static void OnTitleChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((HeaderLabel)sender).SetTitle((string)e.NewValue);
        }

        private void SetTitle(string value)
        {
            if (_txtTitle != null)
                _txtTitle.Text = value;
        }
    }
}
