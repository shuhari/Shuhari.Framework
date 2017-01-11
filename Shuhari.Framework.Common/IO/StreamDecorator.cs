using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            _innerstream = innerStream;
        }

        private readonly Stream _innerstream;

        /// <inheritdoc />
        public override bool CanRead
        {
            get { return _innerstream.CanRead; }
        }

        /// <inheritdoc />
        public override bool CanSeek
        {
            get { return _innerstream.CanSeek; }
        }

        /// <inheritdoc />
        public override bool CanWrite
        {
            get { return _innerstream.CanWrite; }
        }

        /// <inheritdoc />
        public override long Length
        {
            get { return _innerstream.Length; }
        }

        /// <inheritdoc />
        public override long Position
        {
            get { return _innerstream.Position; }
            set { _innerstream.Position = value; }
        }

        /// <inheritdoc />
        public override void Flush()
        {
            _innerstream.Flush();
        }

        /// <inheritdoc />
        public override long Seek(long offset, SeekOrigin origin)
        {
            return _innerstream.Seek(offset, origin);
        }

        /// <inheritdoc />
        public override void SetLength(long value)
        {
            _innerstream.SetLength(value);
        }

        /// <inheritdoc />
        public override int Read(byte[] buffer, int offset, int count)
        {
            return _innerstream.Read(buffer, offset, count);
        }

        /// <inheritdoc />
        public override void Write(byte[] buffer, int offset, int count)
        {
            _innerstream.Write(buffer, offset, count);
        }
    }
}
