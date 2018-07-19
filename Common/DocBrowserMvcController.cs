using DotNetNuke.Web.Mvc.Framework.Controllers;
using DotNetNuke.Web.Mvc.Routing;
using System.Web.Mvc;
using System.Web.Routing;

namespace Connect.DNN.Modules.DocBrowser.Common
{
    public class DocBrowserMvcController : DnnController
    {

        private ContextHelper _docbrowserModuleContext;
        public ContextHelper DocBrowserModuleContext
        {
            get { return _docbrowserModuleContext ?? (_docbrowserModuleContext = new ContextHelper(this)); }
        }

    }
}