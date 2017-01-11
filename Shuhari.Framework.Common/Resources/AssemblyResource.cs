using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using Shuhari.Framework.Globalization;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Resources
{
    /// <summary>
    /// Locate a resource in assembly.
    /// </summary>
    /// <remarks>Resource should set build action to resource, not embed resource.</remarks>
    public sealed class AssemblyResource
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="resourceResourcePath"></param>
        public AssemblyResource(Assembly assembly, string resourceResourcePath)
        {
            Expect.IsNotNull(assembly, nameof(assembly));
            Expect.IsNotBlank(resourceResourcePath, nameof(resourceResourcePath));

            this.Assembly = assembly;
            this.ResourcePath = resourceResourcePath;
        }

        /// <summary>
        /// Assembly
        /// </summary>
        public Assembly Assembly { get; private set; }

        /// <summary>
        /// ResourcePath of file
        /// </summary>
        public string ResourcePath { get; private set; }

        /// <summary>
        /// Open stream for read
        /// </summary>
        /// <returns></returns>
        public Stream OpenRead()
        {
            var genPath = Assembly.GetName().Name + ".g.resources";
            using (var genStream = Assembly.GetManifestResourceStream(genPath))
            {
                var genReader = new ResourceReader(genStream);
                foreach (var entry in genReader.Cast<DictionaryEntry>())
                {
                    var key = (string)entry.Key;
                    if (key.EqualsNoCase(ResourcePath))
                    {
                        return (Stream)entry.Value;
                    }
                }
            }

            throw ExceptionBuilder.Resource(FrameworkStrings.ErrorResourceNotFound,
                Assembly.GetName().Name, ResourcePath);
        }

        /// <summary>
        /// Find all resources in asssembly
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        internal static IEnumerable<AssemblyResource> FindAll(Assembly assembly)
        {
            var result = new List<AssemblyResource>();

            var genPath = assembly.GetName().Name + ".g.resources";
            using (var genStream = assembly.GetManifestResourceStream(genPath))
            {
                var genReader = new ResourceReader(genStream);
                foreach (var entry in genReader.Cast<DictionaryEntry>())
                {
                    var key = (string)entry.Key;
                    result.Add(new AssemblyResource(assembly, key));
                }
            }

            return result.ToArray();
        }

        /// <summary>
        /// Read all bytes content from resource
        /// </summary>
        /// <returns></returns>
        public byte[] ReadAllBytes()
        {
            using (var stream = OpenRead())
            {
                return stream.ReadToEnd();
            }
        }

        /// <summary>
        /// Read all text content
        /// </summary>
        /// <param name="encoding">Text encoding,
        /// or <see cref="EncodingUtil.DefaultEncoding"/> if not specified.</param>
        /// <returns></returns>
        public string ReadAllText(Encoding encoding = null)
        {
            encoding = encoding ?? EncodingUtil.DefaultEncoding;
            return encoding.GetString(ReadAllBytes());
        }

        /// <summary>
        /// Copy content to output file.
        /// </summary>
        /// <param name="filePath">output file path.</param>
        public void CopyToFile(string filePath)
        {
            Expect.IsNotBlank(filePath, nameof(filePath));

            if (File.Exists(filePath))
                File.Delete(filePath);

            using (var ifs = OpenRead())
            using (var ofs = File.OpenWrite(filePath))
            {
                ifs.CopyTo(ofs);
            }
        }

        /// <summary>
        /// Copy content to AppDomain's base directory, keeping file name.
        /// </summary>
        /// <returns>Output file path</returns>
        public string CopyToBaseDirectory()
        {
            var outputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                Path.GetFileName(ResourcePath));
            CopyToFile(outputPath);
            return outputPath;
        }
    }
}