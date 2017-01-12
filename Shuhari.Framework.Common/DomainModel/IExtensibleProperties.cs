namespace Shuhari.Framework.DomainModel
{
    /// <summary>
    /// Object with extensible properties
    /// </summary>
    public interface IExtensibleProperties
    {
        /// <summary>
        /// Extensible properties
        /// </summary>
        /// <param name="propName"></param>
        /// <returns></returns>
        object this[string propName] { get;  set; }
    }
}
