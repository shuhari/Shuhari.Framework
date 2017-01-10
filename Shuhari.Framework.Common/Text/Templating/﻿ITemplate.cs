namespace Shuhari.Framework.Text.Templating
{
    /// <summary>
    /// Interface for text templating
    /// </summary>
    public interface ITemplate
    {
        /// <summary>
        /// Set template parameter
        /// </summary>
        /// <param name="paramName">Parameter name</param>
        /// <param name="value">Parameter value</param>
        /// <returns>This template</returns>
        ITemplate Set(string paramName, object value);

        /// <summary>
        /// Render template to string
        /// </summary>
        /// <returns></returns>
        string Evaluate();
    }
}
