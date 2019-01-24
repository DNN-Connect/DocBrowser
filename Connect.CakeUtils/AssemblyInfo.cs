using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Connect.CakeUtils
{
    public class AssemblyInfo
    {
        private string FilePath { get; set; }
        private Dictionary<int, string> Lines { get; set; } = new Dictionary<int, string>();
        private Dictionary<string, string> StringProperties = new Dictionary<string, string>();
        private Dictionary<int, string> StringPropLines = new Dictionary<int, string>();
        public AssemblyInfo(string filePath)
        {
            FilePath = filePath;
            var lineNr = 0;
            using (var sr = new StreamReader(filePath, System.Text.Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    Lines[lineNr] = line;
                    var m = Regex.Match(line, @"^\[assembly\: ([^\(]+)\(""(.*)\""\)\]");
                    if (m.Success)
                    {
                        var prop = m.Groups[1].Value.Trim();
                        var val = m.Groups[2].Value;
                        StringProperties[prop] = val;
                        StringPropLines[lineNr] = prop;
                    }
                    lineNr++;
                }
            }
        }
        public void SetProperty(string propName, string propValue)
        {
            if (!StringProperties.ContainsKey(propName))
            {
                StringPropLines[Lines.Count] = propName;
                Lines[Lines.Count] = "";
            }
            StringProperties[propName] = propValue;
        }
        public void Write()
        {
            Write(FilePath);
        }
        public void Write(string filePath)
        {
            using (var sw = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
            {
                for (var lineNr = 0; lineNr < Lines.Count; lineNr++)
                {
                    if (StringPropLines.ContainsKey(lineNr))
                    {
                        sw.WriteLine(string.Format("[assembly: {0}(\"{1}\")]", StringPropLines[lineNr], StringProperties[StringPropLines[lineNr]]));
                    }
                    else
                    {
                        sw.WriteLine(Lines[lineNr]);
                    }
                }
            }
        }
    }
}
