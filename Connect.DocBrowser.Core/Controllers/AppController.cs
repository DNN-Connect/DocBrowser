using Connect.DocBrowser.Core.Common;
using Connect.DocBrowser.Core.Models;
using Connect.DocBrowser.Core.Models.Items;
using Connect.DocBrowser.Core.Repositories;
using DotNetNuke.Entities.Portals;
using Markdig;
using Markdig.Extensions.Yaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Connect.DocBrowser.Core.Controllers
{
    public class AppController
    {
        public static void Reset(PortalSettings portalSettings, int moduleId)
        {
            ItemRepository.Instance.DeleteItemsByModuleId(moduleId);
            var rootDir = Path.Combine(portalSettings.HomeDirectoryMapPath, "Docs\\" + moduleId.ToString());
            if (!Directory.Exists(rootDir))
            {
                Directory.CreateDirectory(rootDir);
                return;
            }
            //var menuFile = Path.Combine(rootDir, "menu.json");
            var contentsDir = Path.Combine(rootDir, "content");
            if (!Directory.Exists(contentsDir))
            {
                Directory.CreateDirectory(contentsDir);
                return;
            }
            ParseDir(new DirectoryInfo(contentsDir), portalSettings.HomeDirectory + "Docs/" + moduleId.ToString() + "/content/", moduleId);
        }
        private static void ParseDir(DirectoryInfo directory, string relativeDir, int moduleId)
        {
            var pipeline = new MarkdownPipelineBuilder().UseYamlFrontMatter().Build();
            using (var cons = new StreamWriter("D:\\Webroot\\DNNAPI\\_dev\\out.txt", true, Encoding.UTF8))
            {
                foreach (var f in directory.GetFiles("*.md"))
                {
                    var content = "";
                    using (var sr = new StreamReader(f.FullName))
                    {
                        content = sr.ReadToEnd();
                    }
                    if (!string.IsNullOrEmpty(content))
                    {
                        var block = new FrontMatterBlock();
                        cons.WriteLine(f.FullName);
                        var md = Markdown.Parse(content, pipeline);
                        //md.GetData("");
                        cons.WriteLine(content);
                        foreach (var b in md)
                        {
                            if (b is YamlFrontMatterBlock)
                            {
                                var fm = (YamlFrontMatterBlock)b;
                                block = fm.Parse();
                                cons.WriteLine(block);
                            }
                        }
                        if (block.Parsed)
                        {
                            var text = Markdown.ToHtml(content, pipeline);
                            text = Regex.Replace(text, "src=\"([^\"]+)\"", m =>
                            {
                                return "src=\"" + relativeDir + m.Groups[1] + "\"";
                            });
                            text = Regex.Replace(text, "href=\"([^/\"]+)\"", m =>
                            {
                                return "href=\"#\" data-topic=\"" + m.Groups[1] + "\"";
                            });
                            cons.WriteLine(text);
                            var itm = new Item()
                            {
                                Contents = text,
                                Edition = block.Edition(),
                                Locale = block.Locale,
                                ModuleId = moduleId,
                                NextTopic = block.NextTopic,
                                ParentTopic = block.ParentTopic,
                                PreviousTopic = block.PreviousTopic,
                                Title = block.Title,
                                Topic = block.Topic,
                                Version = block.Version
                            };
                            ItemRepository.Instance.AddItem(itm);
                        }
                    }
                }
                cons.Flush();
            }
            foreach (var subDir in directory.GetDirectories())
            {
                if (!subDir.Name.StartsWith("."))
                {
                    ParseDir(subDir, relativeDir + subDir.Name + "/", moduleId);
                }
            }
        }
    }
}
