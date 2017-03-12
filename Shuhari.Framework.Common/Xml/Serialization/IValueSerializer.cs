namespace Shuhari.Framework.Xml.Serialization
{
    /// <summary>
    /// Serialize value to xml
    /// </summary>
    public interface IValueSerializer
    {
        /// <summary>
        /// Serialize clr value to xml attribute
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        string Serialize(object value);

        /// <summary>
        /// Deserialize from xml
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        object Deserialize(string str);
    }
}
