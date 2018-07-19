using System;
using System.Collections.Generic;
using DotNetNuke.Common;
using DotNetNuke.Data;
using DotNetNuke.Framework;
using Connect.DocBrowser.Core.Models.Items;

namespace Connect.DocBrowser.Core.Repositories
{

	public partial class ItemRepository : ServiceLocator<IItemRepository, ItemRepository>, IItemRepository
 {
        protected override Func<IItemRepository> GetFactory()
        {
            return () => new ItemRepository();
        }
        public Item GetItem(int moduleId, string topic, string locale, int edition, string version)
        {
            using (var context = DataContext.Instance())
            {
                return context.ExecuteSingleOrDefault<Item>(System.Data.CommandType.Text,
                    "SELECT * FROM {databaseOwner}{objectQualifier}Connect_DocBrowser_Items WHERE ModuleId=@0 AND Topic=@1 AND Locale=@2 AND Edition=@3 AND Version=@4",
                    moduleId,topic,locale,edition,version);
            }
        }
        public void AddItem(Item item)
        {
            Requires.NotNull(item);
            using (var context = DataContext.Instance())
            {
                context.Execute(System.Data.CommandType.Text,
                    "IF NOT EXISTS (SELECT * FROM {databaseOwner}{objectQualifier}Connect_DocBrowser_Items " +
                    "WHERE ModuleId=@0 AND Topic=@1 AND Locale=@2 AND Edition=@3 AND Version=@4) " +
                    "INSERT INTO {databaseOwner}{objectQualifier}Connect_DocBrowser_Items (ModuleId, Topic, Locale, Edition, Version, Title, ParentTopic, PreviousTopic, NextTopic, Contents) " +
                    "SELECT @0, @1, @2, @3, @4, @5, @6, @7, @8, @9", item.ModuleId, item.Topic, item.Locale, item.Edition, item.Version, item.Title, item.ParentTopic, item.PreviousTopic, item.NextTopic, item.Contents);
            }
        }
        public void DeleteItem(Item item)
        {
            DeleteItem(item.ModuleId, item.Topic, item.Locale, item.Edition, item.Version);
        }
        public void DeleteItem(int moduleId, string topic, string locale, int edition, string version)
        {
            using (var context = DataContext.Instance())
            {
                context.Execute(System.Data.CommandType.Text,
                    "DELETE FROM {databaseOwner}{objectQualifier}Connect_DocBrowser_Items WHERE ModuleId=@0 AND Topic=@1 AND Locale=@2 AND Edition=@3 AND Version=@4",
                    moduleId,topic,locale,edition,version);
            }
        }
        public void UpdateItem(Item item)
        {
            Requires.NotNull(item);
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<Item>();
                rep.Update("SET Title=@0, ParentTopic=@1, PreviousTopic=@2, NextTopic=@3, Contents=@4 WHERE ModuleId=@5 AND Topic=@6 AND Locale=@7 AND Edition=@8 AND Version=@9",
                          item.Title,item.ParentTopic,item.PreviousTopic,item.NextTopic,item.Contents, item.ModuleId,item.Topic,item.Locale,item.Edition,item.Version);
            }
        } 
 }

    public partial interface IItemRepository
    {
        Item GetItem(int moduleId, string topic, string locale, int edition, string version);
        void AddItem(Item item);
        void DeleteItem(Item item);
        void DeleteItem(int moduleId, string topic, string locale, int edition, string version);
        void UpdateItem(Item item);
    }
}

