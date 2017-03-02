using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Markup;
using Shuhari.Framework.Wpf;

[assembly: AssemblyTitle("Shuhari.Framework.Wpf")]
[assembly: AssemblyDescription("WPF Extensions")]
[assembly: Guid("f42283da-f5dc-4f3e-89b8-e5d0e25043c6")]

[assembly: XmlnsDefinition(WpfAssemblyInfo.XAML_NAMESPACE, "Shuhari.Framework.Wpf.Controls")]
[assembly: XmlnsPrefix(WpfAssemblyInfo.XAML_NAMESPACE, WpfAssemblyInfo.XAML_PREFIX)]

namespace Shuhari.Framework.Wpf
{
    /// <summary>
    /// Assembly constants
    /// </summary>
    public static class WpfAssemblyInfo
    {
        /// <summary>
        /// XAML namespace
        /// </summary>
        public const string XAML_NAMESPACE = "https://shuhari.github.io/schemas/shuhari.framework/wpf/2017";

        /// <summary>
        /// XAML prefix
        /// </summary>
        public const string XAML_PREFIX = "sw";
    }
}