using Cake.Core.IO;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Connect.CakeUtils
{
    public static class Extensions
    {
        public static string ToNormalizedVersion(this Version version)
        {
            return string.Format("{0:00}.{1:00}.{2:00}", version.Major, version.Minor, version.Build);
        }
        public static string ToNormalizedVersion(this string version)
        {
            return new Version(version).ToNormalizedVersion();
        }
        public static string ToShortVersion(this string version)
        {
            return new Version(version).ToString();
        }
        public static string Serialize<T>(this T value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            try
            {
                var xmlserializer = new XmlSerializer(typeof(T));
                var stringWriter = new StringWriter();
                using (var writer = XmlWriter.Create(stringWriter))
                {
                    xmlserializer.Serialize(writer, value);
                    return stringWriter.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw new Exception("An error occurred", ex);
            }
        }
        public static XmlDocument ToXmlDocument(this string value)
        {
            var res = new XmlDocument();
            res.LoadXml(value);
            return res;
        }
        public static bool Contains(this FilePathCollection input, string filePath)
        {
            filePath = filePath.Replace("\\", "/");
            foreach (var fp in input)
            {
                if (fp.FullPath == filePath)
                {
                    return true;
                }
            }
            return false;
        }
        public static string EnsureStartsWith(this string input, string start)
        {
            if (!input.StartsWith(start))
            {
                return start + input;
            }
            return input;
        }
        public static string EnsureEndsWith(this string input, string end)
        {
            if (!input.EndsWith(end))
            {
                return input + end;
            }
            return input;
        }
    }
}
