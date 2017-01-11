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
        public static FileItem ComparessFiles(string fileName, IEnumerable<FileItem> files)
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
    }
}
