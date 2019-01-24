using System.IO;

namespace Connect.CakeUtils
{
    public class Markdown
    {
        public static string ToHtml(string fileName)
        {
            if (File.Exists(fileName))
            {
                var input = "";
                using (var sr = new StreamReader(fileName))
                {
                    input = sr.ReadToEnd();
                }
                return Markdig.Markdown.ToHtml(input);
            }
            else
            {
                return "";
            }
        }
    }
}
