
using Connect.DocBrowser.Core.Common;
using Connect.DocBrowser.Core.Repositories;
using System;
using System.Collections.Generic;

namespace Connect.DocBrowser.Core.Models.Items
{
    public partial class Item
 {
        public Dictionary<string, string> CompareWith(Item item)
        {
            var res = new Dictionary<string, string>();
            if (ModuleId != item.ModuleId)
            {
                res.Add("ModuleId", item.ModuleId.ToString());
            }
            if (Topic != item.Topic)
            {
                res.Add("Topic", item.Topic.ToString());
            }
            if (Locale != item.Locale)
            {
                res.Add("Locale", item.Locale.ToString());
            }
            if (Edition != item.Edition)
            {
                res.Add("Edition", item.Edition.ToString());
            }
            if (Version != item.Version)
            {
                res.Add("Version", item.Version.ToString());
            }
            if (Title != item.Title)
            {
                res.Add("Title", item.Title.ToString());
            }
            if (ParentTopic != item.ParentTopic)
            {
                res.Add("ParentTopic", item.ParentTopic.ToString());
            }
            if (PreviousTopic != item.PreviousTopic)
            {
                res.Add("PreviousTopic", item.PreviousTopic.ToString());
            }
            if (NextTopic != item.NextTopic)
            {
                res.Add("NextTopic", item.NextTopic.ToString());
            }
            if (Contents != item.Contents)
            {
                res.Add("Contents", item.Contents.ToString());
            }

            return res;
        }
        public void LogChange(short action, int userId, string columnName, string newValue)
        {
            var changes = new Dictionary<string, string>();
            changes.Add(columnName, newValue);
            LogRepository.Instance.Log((short)LogObjectTypes.Item, ModuleId, Topic, Locale, Edition, Version, action, DateTime.Now, userId, changes);
        }
 }
}

