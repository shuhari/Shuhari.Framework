using System.IO;

namespace Shuhari.Framework.Utils
{
    /// <summary>
    /// Helper methods to process stream
    /// </summary>
    public static class StreamUtil
    {
        /// <summary>
        /// Read stream to end
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] ReadToEnd(this Stream stream)
        {
            Expect.IsNotNull(stream, nameof(stream));

            var bytesToRead = (int)(stream.Length - stream.Position);
            var buf = new byte[bytesToRead];
            stream.Read(buf, 0, bytesToRead);
            return buf;
        }
    }
}
