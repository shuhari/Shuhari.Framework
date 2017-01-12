using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Markup;
using Shuhari.Framework;

[assembly: AssemblyTitle("Shuhari.Framework.Common")]
[assembly: AssemblyDescription("Common functions")]
[assembly: Guid("d3ad108a-1ebb-43ca-9854-32ceb2fcba7f")]

[assembly: InternalsVisibleTo("Shuhari.Framework.UnitTests")]
[assembly: InternalsVisibleTo("Shuhari.Framework.IntegrationTests")]

[assembly: XmlnsDefinition(AssemblyInfo.XAML_NAMESPACE, "Shuhari.Framework.DomainModel")]
[assembly: XmlnsPrefix(AssemblyInfo.XAML_NAMESPACE, AssemblyInfo.XAML_PREFIX)]


namespace Shuhari.Framework
{
    /// <summary>
    /// Global constants
    /// </summary>
    public static class AssemblyInfo
    {
        /// <summary>
        /// Xaml namespace
        /// </summary>
        public const string XAML_NAMESPACE = "https://shuhari.github.io/schemas/shuhari.framework/xaml/2017";

        /// <summary>
        /// Xaml prefix
        /// </summary>
        public const string XAML_PREFIX = "f";
    }
}
