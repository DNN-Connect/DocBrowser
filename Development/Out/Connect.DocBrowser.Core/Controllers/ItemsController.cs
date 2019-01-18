
using Connect.DocBrowser.Core.Models.Items;
using Connect.DocBrowser.Core.Repositories;

namespace Connect.DocBrowser.Core.Controllers
{

 public partial class ItemsController
 {


  public static void UpdateItem(ItemBase item)
  {

   ItemBaseRepository repo = new ItemBaseRepository();
   repo.Update(item);

  }

  public static void DeleteItem(ItemBase item)
  {

   ItemBaseRepository repo = new ItemBaseRepository();
   repo.Delete(item);

  }

 }
}
