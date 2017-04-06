using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Shuhari.Framework.Wpf.Controls
{
    /// <summary>
    /// Status label
    /// </summary>
    [TemplatePart(Name = "Part_Border", Type = typeof(Border))]
    [TemplatePart(Name = "Part_Text", Type = typeof(TextBlock))]
    public class StatusLabel : Control
    {
        static StatusLabel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StatusLabel),
                new FrameworkPropertyMetadata(typeof(StatusLabel)));

            _defaultInfoBackground = new SolidColorBrush(Colors.LightGreen);
            _defaultErrorBackground = new SolidColorBrush(Colors.LightPink);
        }

        /// <summary>
        /// Initialize
        /// </summary>
        public StatusLabel()
        {
        }

        private static readonly Brush _defaultInfoBackground;
        private static readonly Brush _defaultErrorBackground;

        private Border _border;
        private TextBlock _txt;

        /// <inheritdoc />
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _border = Template.FindName("Part_Border", this) as Border;
            _txt = Template.FindName("Part_Text", this) as TextBlock;
        }


        /// <summary>
        /// Background to show information
        /// </summary>
        public Brush InfoBackground
        {
            get { return (Brush)GetValue(InfoBackgroundProperty); }
            set { SetValue(InfoBackgroundProperty, value); }
        }

        /// <summary>
        /// InfoBackground property
        /// </summary>
        public static readonly DependencyProperty InfoBackgroundProperty =
            DependencyProperty.Register(nameof(InfoBackground), 
                typeof(Brush), typeof(StatusLabel), 
                new PropertyMetadata(_defaultInfoBackground));

        /// <summary>
        /// Background for error message
        /// </summary>
        public Brush ErrorBackground
        {
            get { return (Brush)GetValue(ErrorBackgroundProperty); }
            set { SetValue(ErrorBackgroundProperty, value); }
        }

        /// <summary>
        /// ErrorBackground property
        /// </summary>
        public static readonly DependencyProperty ErrorBackgroundProperty =
            DependencyProperty.Register(nameof(ErrorBackground), 
                typeof(Brush), typeof(StatusLabel), 
                new PropertyMetadata(_defaultErrorBackground));


        /// <summary>
        /// Show normal message
        /// </summary>
        /// <param name="msg"></param>
        public void ShowInfo(string msg)
        {
            ShowMsg(true, msg);
        }

        /// <summary>
        /// Show error message
        /// </summary>
        /// <param name="msg"></param>
        public void ShowError(string msg)
        {
            ShowMsg(false, msg);
        }

        /// <summary>
        /// Show message
        /// </summary>
        /// <param name="success"></param>
        /// <param name="msg"></param>
        private void ShowMsg(bool success, string msg)
        {
            if (_border != null)
            {
                _border.Background = success ? _defaultInfoBackground : _defaultErrorBackground;
            }
            if (_txt != null)
            {
                _txt.Text = msg;
            }
            this.Show();
        }
    }
}
