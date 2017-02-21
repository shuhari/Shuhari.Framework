using System.IO;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.IO
{
    /// <summary>
    /// Decorate stream, delegate actual work to inner stream, but can also be overriden.
    /// Useful for Web filters and so on.
    /// </summary>
    public class StreamDecorator : Stream
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="innerStream"></param>
        public StreamDecorator(Stream innerStream)
        {
            Expect.IsNotNull(innerStream, nameof(innerStream));

            InnerStream = innerStream;
        }

        /// <summary>
        /// Inner stream
        /// </summary>
        public Stream InnerStream { get; private set; }

        /// <inheritdoc />
        public override bool CanRead
        {
            get { return InnerStream.CanRead; }
        }

        /// <inheritdoc />
        public override bool CanSeek
        {
            get { return InnerStream.CanSeek; }
        }

        /// <inheritdoc />
        public override bool CanWrite
        {
            get { return InnerStream.CanWrite; }
        }

        /// <inheritdoc />
        public override long Length
        {
            get { return InnerStream.Length; }
        }

        /// <inheritdoc />
        public override long Position
        {
            get { return InnerStream.Position; }
            set { InnerStream.Position = value; }
        }

        /// <inheritdoc />
        public override void Flush()
        {
            InnerStream.Flush();
        }

        /// <inheritdoc />
        public override long Seek(long offset, SeekOrigin origin)
        {
            return InnerStream.Seek(offset, origin);
        }

        /// <inheritdoc />
        public override void SetLength(long value)
        {
            InnerStream.SetLength(value);
        }

        /// <inheritdoc />
        public override int Read(byte[] buffer, int offset, int count)
        {
            return InnerStream.Read(buffer, offset, count);
        }

        /// <inheritdoc />
        public override void Write(byte[] buffer, int offset, int count)
        {
            InnerStream.Write(buffer, offset, count);
        }
    }
}
