using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Connect.DocBrowser.Core.Common
{
    public class FilePath
    {
        public string FullPath { get; set; }
        public bool HasExtension { get; set; }
        public bool IsRelative { get; set; }
        public string[] Segments { get; set; }
    }

    public static class Compression
    {
        public static void AddFilesToZip(string start, string zipFile, IEnumerable<FilePath> files)
        {
            AddFilesToZip(start, zipFile, files, false);
        }
        public static void AddFilesToZip(string start, string zipFile, IEnumerable<FilePath> files, bool append)
        {
            var root = new DirectoryInfo(start);
            var rootPath = root.FullName;
            using (var outStream = new ZipArchive(File.Create(zipFile), append ? ZipArchiveMode.Update : ZipArchiveMode.Create))
            {
                {
                    foreach (var file in files)
                    {
                        var entry = outStream.CreateEntry(file.FullPath.Substring(rootPath.Length));
                        AddFile(file.FullPath, entry.Open());
                    }
                }
            }
        }

        public static Stream ZipToStream(string start, IEnumerable<FilePath> files)
        {
            var root = new DirectoryInfo(start);
            var rootPath = root.FullName;
            var res = new MemoryStream();
            using (var outStream = new ZipArchive(res))
            {
                foreach (var file in files)
                {
                    var entry = outStream.CreateEntry(file.FullPath.Substring(rootPath.Length));
                    AddFile(file.FullPath, entry.Open());
                }
            }
            return res;
        }

        public static void AddFile(string sourcePath, Stream zip)
        {
            using (FileStream src = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
            {
                CopyStream(src, zip, 25000);
            }
        }

        public static void CopyStream(Stream source, Stream destination, Int32 bufferSize)
        {
            byte[] bBuffer = new byte[bufferSize + 1];
            Int32 iLengthOfReadChunk;
            do
            {
                iLengthOfReadChunk = source.Read(bBuffer, 0, bufferSize);
                destination.Write(bBuffer, 0, iLengthOfReadChunk);
                if (iLengthOfReadChunk == 0)
                {
                    break;
                }
            }
            while (true);
        }

    }
}
