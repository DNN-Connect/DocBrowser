
using System;
using System.Runtime.Serialization;
using DotNetNuke.ComponentModel.DataAnnotations;
using Connect.DocBrowser.Core.Data;

namespace Connect.DocBrowser.Core.Models.Items
{
    [TableName("Connect_DocBrowser_Items")]
    [DataContract]
    [Scope("ModuleId")]
    public partial class Item     {

        #region .ctor
        public Item()
        {
        }
        #endregion

        #region Properties
        [DataMember]
        public int ModuleId { get; set; }
        [DataMember]
        public string Topic { get; set; }
        [DataMember]
        public string Locale { get; set; }
        [DataMember]
        public int Edition { get; set; }
        [DataMember]
        public string Version { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string ParentTopic { get; set; }
        [DataMember]
        public string PreviousTopic { get; set; }
        [DataMember]
        public string NextTopic { get; set; }
        [DataMember]
        public string Contents { get; set; }
        #endregion

        #region Methods
        public void ReadItem(Item item)
        {
            if (item.ModuleId > -1)
                ModuleId = item.ModuleId;

            if (!String.IsNullOrEmpty(item.Topic))
                Topic = item.Topic;

            if (!String.IsNullOrEmpty(item.Locale))
                Locale = item.Locale;

            if (item.Edition > -1)
                Edition = item.Edition;

            if (!String.IsNullOrEmpty(item.Version))
                Version = item.Version;

            if (!String.IsNullOrEmpty(item.Title))
                Title = item.Title;

            if (!String.IsNullOrEmpty(item.ParentTopic))
                ParentTopic = item.ParentTopic;

            if (!String.IsNullOrEmpty(item.PreviousTopic))
                PreviousTopic = item.PreviousTopic;

            if (!String.IsNullOrEmpty(item.NextTopic))
                NextTopic = item.NextTopic;

            if (!String.IsNullOrEmpty(item.Contents))
                Contents = item.Contents;

        }
        #endregion

    }
}



