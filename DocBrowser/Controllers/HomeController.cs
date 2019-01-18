using System.Web.Mvc;
using Connect.DNN.Modules.DocBrowser.Common;

namespace Connect.DNN.Modules.DocBrowser.Controllers
{
    public class HomeController: DocBrowserMvcController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}
