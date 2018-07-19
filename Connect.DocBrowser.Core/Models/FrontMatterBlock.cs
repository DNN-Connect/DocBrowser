using Connect.DocBrowser.Core.Common;
using System;
using System.Collections.Generic;

namespace Connect.DocBrowser.Core.Models
{
    public class FrontMatterBlock
    {
        public string Topic { get; set; }
        public string Locale { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
        public string[] Editions { get; set; }
        public string ParentTopic { get; set; }
        public string PreviousTopic { get; set; }
        public string NextTopic { get; set; }
        public string[] RelatedTopics { get; set; }
        public bool Parsed { get; set; } = false;

        public FrontMatterBlock() { }
        public FrontMatterBlock(IDictionary<string, string> frontMatter)
        {
            try
            {
                Topic = frontMatter["topic"].Trim();
                Locale = frontMatter["locale"].Trim();
                Title = frontMatter["title"].Trim();
                Version = frontMatter["dnnversion"].Trim();
                Editions = frontMatter["dnneditions"].Trim().Split(',');
                if (frontMatter.ContainsKey("parent-topic")) ParentTopic = frontMatter["parent-topic"].Trim();
                if (frontMatter.ContainsKey("previous-topic")) PreviousTopic = frontMatter["previous-topic"].Trim();
                if (frontMatter.ContainsKey("next-topic")) NextTopic = frontMatter["next-topic"].Trim();
                if (frontMatter.ContainsKey("related-topics")) RelatedTopics = frontMatter["related-topics"].Trim().Split(',');
            }
            catch (Exception)
            {
                Parsed = false;
            }
            Parsed = true;
        }

        public int Edition()
        {
            var res = (int)DnnEditions.All;
            foreach (var e in Editions)
            {
                switch (e.Trim().ToLower())
                {
                    case "dnn platform":
                    case "platform":
                        res = res | (int)DnnEditions.DnnPlatform;
                        break;
                    case "evoq content":
                        res = res | (int)DnnEditions.EvoqContent;
                        break;
                    case "evoq engage":
                        res = res | (int)DnnEditions.EvoqEngage;
                        break;
                }
            }
            return res;
        }

    }
}
