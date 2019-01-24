using Cake.Core.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Xml;

namespace Connect.CakeUtils
{
    public static class Compression
    {
        public static void AddBinaryFileToZip(string zipFile, byte[] content, string name)
        {
            AddBinaryFileToZip(zipFile, content, name, false);
        }
        public static void AddBinaryFileToZip(string zipFile, byte[] content, string name, bool append)
        {
            using (var sr = new MemoryStream(content))
            {
                AddStreamToZip(zipFile, sr, name, append);
            }
        }
        public static void AddXmlFileToZip(string zipFile, XmlDocument content, string name)
        {
            AddXmlFileToZip(zipFile, content, name, false);
        }
        public static void AddXmlFileToZip(string zipFile, XmlDocument content, string name, bool append)
        {
            using (var ms = new MemoryStream())
            {
                var writer = new XmlTextWriter(ms, System.Text.Encoding.UTF8);
                writer.Formatting = Formatting.Indented;
                content.WriteContentTo(writer);
                writer.Flush();
                ms.Flush();
                ms.Position = 0;
                AddStreamToZip(zipFile, ms, name, append);
            }
        }
        public static void AddTextFileToZip(string zipFile, string content, string name)
        {
            AddTextFileToZip(zipFile, content, name, false);
        }
        public static void AddTextFileToZip(string zipFile, string content, string name, bool append)
        {
            using (var sr = GenerateStreamFromString(content))
            {
                AddStreamToZip(zipFile, sr, name, append);
            }
        }
        public static void AddFilesToZip(string zipFile, FilePathCollection files)
        {
            AddFilesToZip(zipFile, ".", "", files, false);
        }
        public static void AddFilesToZip(string zipFile, FilePathCollection files, bool append)
        {
            AddFilesToZip(zipFile, ".", "", files, append);
        }
        public static void AddFilesToZip(string zipFile, string start, FilePathCollection files)
        {
            AddFilesToZip(zipFile, start, "", files, false);
        }
        public static void AddFilesToZip(string zipFile, string start, FilePathCollection files, bool append)
        {
            AddFilesToZip(zipFile, start, "", files, append);
        }
        public static void AddFilesToZip(string zipFile, string start, string newStart, FilePathCollection files)
        {
            AddFilesToZip(zipFile, start, newStart, files, false);
        }
        public static void AddFilesToZip(string zipFile, string start, string newStart, FilePathCollection files, bool append)
        {
            if (!append)
            {
                if (File.Exists(zipFile))
                {
                    File.Delete(zipFile);
                }
            }
            if (newStart != "")
            {
                newStart = newStart.EnsureEndsWith("/");
            }
            var root = new DirectoryInfo(start);
            var rootPath = root.FullName;
            foreach (var file in files)
            {
                Console.WriteLine("Reading " + file.FullPath);
                using (var fileToZip = new FileStream(file.FullPath, FileMode.Open, FileAccess.Read))
                {
                    var pathInZip = newStart == "" ?
                        file.FullPath.Substring(rootPath.Length + 1) :
                        newStart + file.FullPath.Substring(rootPath.Length + 1);
                    AddStreamToZip(zipFile, fileToZip, pathInZip, true);
                }
            }
        }
        public static void AddStreamToZip(string zipFile, Stream fileToZip, string name)
        {
            AddStreamToZip(zipFile, fileToZip, name, false);
        }
        public static void AddStreamToZip(string zipFile, Stream fileToZip, string name, bool append)
        {
            if (!append)
            {
                if (File.Exists(zipFile))
                {
                    File.Delete(zipFile);
                }
            }
            using (var outFile = File.Open(zipFile, FileMode.OpenOrCreate))
            {
                using (var outStream = new ZipArchive(outFile, append ? ZipArchiveMode.Update : ZipArchiveMode.Create))
                {
                    Console.WriteLine("Zipping " + name);
                    var entry = outStream.CreateEntry(name);
                    fileToZip.CopyTo(entry.Open());
                }
            }
        }

        public static Stream ZipToStream(string start, IEnumerable<FilePath> files)
        {
            var root = new DirectoryInfo(start);
            var rootPath = root.FullName;
            var res = new MemoryStream();
            var outStream = new ZipArchive(res, ZipArchiveMode.Create, true);
            foreach (var file in files)
            {
                Console.WriteLine("Zipping " + file.FullPath);
                outStream.CreateEntryFromFile(file.FullPath, file.FullPath.Substring(rootPath.Length + 1));
            }
            res.Seek(0, SeekOrigin.Begin);
            return res;
        }

        public static byte[] ZipToBytes(string start, IEnumerable<FilePath> files)
        {
            var root = new DirectoryInfo(start);
            var rootPath = root.FullName;
            byte[] compressedBytes;

            using (var outStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(outStream, ZipArchiveMode.Create, true))
                {
                    foreach (var file in files)
                    {
                        Console.WriteLine("Zipping " + file.FullPath);
                        var fileInArchive = archive.CreateEntry(file.FullPath.Substring(rootPath.Length + 1), CompressionLevel.Optimal);
                        using (var entryStream = fileInArchive.Open())
                        using (var fileToCompressStream = new FileStream(file.FullPath, FileMode.Open, FileAccess.Read))
                        {
                            fileToCompressStream.CopyTo(entryStream);
                        }
                    }
                }
                compressedBytes = outStream.ToArray();
            }
            return compressedBytes;
        }

        public static void AddFile(string sourcePath, Stream zip)
        {
            using (FileStream src = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
            {
                src.CopyToAsync(zip);
            }
        }

        public static void CopyStream(Stream source, Stream destination, int bufferSize)
        {
            byte[] bBuffer = new byte[bufferSize + 1];
            int iLengthOfReadChunk;
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

        public static MemoryStream GenerateStreamFromString(string value)
        {
            return new MemoryStream(System.Text.Encoding.UTF8.GetBytes(value ?? ""));
        }

        public static void SaveStream(Stream input, string filePath)
        {
            if (input.CanSeek)
            {
                input.Seek(0, SeekOrigin.Begin);
            }
            using (var outFile = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                input.CopyTo(outFile);
            }
        }

        public static void SaveBytes(byte[] input, string filePath)
        {
            using (var outFile = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
            using (var fileToSave = new MemoryStream(input))
            {
                fileToSave.CopyTo(outFile);
            }
        }

    }
}
