using Connect.DocBrowser.Core.Models;
using Markdig.Extensions.Yaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect.DocBrowser.Core.Common
{
    public static class MarkdownExtensions
    {
        public static FrontMatterBlock Parse(this YamlFrontMatterBlock block)
        {
            var lines = new Dictionary<string, string>();
            foreach (var l in block.Lines.Lines.Select(l => l.ToString()))
            {
                if (l.IndexOf(':')>0)
                {
                    lines[l.Substring(0, l.IndexOf(':'))] = l.Substring(l.IndexOf(':') + 1);
                }
            }
            return new FrontMatterBlock(lines);
        }
    }
}
