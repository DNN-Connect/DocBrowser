using System;
using System.Collections.Generic;
using System.Linq;
using DotNetNuke.Common;
using DotNetNuke.Data;
using DotNetNuke.Framework;
using Connect.DocBrowser.Core.Models.Items;

namespace Connect.DocBrowser.Core.Repositories
{
    public partial class ItemRepository : ServiceLocator<IItemRepository, ItemRepository>, IItemRepository
    {
        public void DeleteItemsByModuleId(int moduleId)
        {
            using (var context = DataContext.Instance())
            {
                context.Execute(System.Data.CommandType.Text,
                    "DELETE FROM {databaseOwner}{objectQualifier}Connect_DocBrowser_Items WHERE ModuleId=@0",
                    moduleId);
            }
        }
        public IEnumerable<string> GetVersionList(int moduleId)
        {
            using (var context = DataContext.Instance())
            {
                return context.ExecuteQuery<string>(System.Data.CommandType.Text,
                    "SELECT DISTINCT i.Version FROM {databaseOwner}{objectQualifier}Connect_DocBrowser_Items i WHERE i.ModuleId=@0 ORDER BY i.Version",
                    moduleId);
            }
        }
    }
    public partial interface IItemRepository
    {
        void DeleteItemsByModuleId(int moduleId);
        IEnumerable<string> GetVersionList(int moduleId);
    }
}

