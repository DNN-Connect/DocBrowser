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
    }
    public partial interface IItemRepository
    {
    }
}

