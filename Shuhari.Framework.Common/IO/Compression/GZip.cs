using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Shuhari.Framework.DomainModel;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.IO.Compression
{
    /// <summary>
    /// Implement GZip compression/decompression
    /// </summary>
    public static class GZip
    {
        /// <summary>
        /// Compress all files to one zip package
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        public static FileItem CompressFiles(string fileName, IEnumerable<FileItem> files)
        {
            Expect.IsNotBlank(fileName, nameof(fileName));
            Expect.IsNotNull(files, nameof(files));

            using (var stream = new MemoryStream())
            {
                using (var archive = new ZipArchive(stream, ZipArchiveMode.Create))
                {
                    foreach (var file in files)
                    {
                        var entry = archive.CreateEntry(file.Name);
                        using (var entryStream = entry.Open())
                        {
                            entryStream.Write(file.Content, 0, file.Content.Length);
                        }
                    }
                }
                return new FileItem(fileName, "", stream.ToArray());
            }
        }

        /// <summary>
        /// Decompress files from package
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static FileItem[] DecompressFiles(byte[] content)
        {
            Expect.IsNotNull(content, nameof(content));

            using (var stream = new MemoryStream(content))
            {
                return DecompressFiles(stream);
            }
        }

        /// <summary>
        /// Decompress files from package
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static FileItem[] DecompressFiles(Stream stream)
        {
            Expect.IsNotNull(stream, nameof(stream));
            var files = new List<FileItem>();

            using (var archive = new ZipArchive(stream, ZipArchiveMode.Read))
            {
                foreach (var entry in archive.Entries)
                {
                    var buf = new byte[entry.Length];
                    using (var entryStream = entry.Open())
                    {
                        entryStream.Read(buf, 0, buf.Length);
                        var file = new FileItem(entry.FullName, "", buf);
                        files.Add(file);
                    }
                }
            }

            return files.ToArray();
        }

        /// <summary>
        /// Compress bytes data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] Compress(byte[] data)
        {
            Expect.IsNotNull(data, nameof(data));

            using (var ms = new MemoryStream())
            {
                using (var zis = new GZipStream(ms, CompressionMode.Compress))
                {
                    zis.Write(data, 0, data.Length);
                    zis.Flush();
                }
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Decompress bytes data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] Decompress(byte[] data)
        {
            Expect.IsNotNull(data, nameof(data));

            using (var mis = new MemoryStream(data))
            {
                using (var zos = new GZipStream(mis, CompressionMode.Decompress))
                using (var mos = new MemoryStream())
                {
                    zos.CopyTo(mos);
                    zos.Flush();
                    return mos.ToArray();
                }
            }
        }
    }
}
