using DotNetNuke.Web.Api;
using System.Net;
using System.Net.Http;

namespace Connect.DNN.Modules.DocBrowser.Common
{
    public class DocBrowserApiController : DnnApiController
    {
        private ContextHelper _docbrowserModuleContext;
        public ContextHelper DocBrowserModuleContext
        {
            get { return _docbrowserModuleContext ?? (_docbrowserModuleContext = new ContextHelper(this)); }
        }

        public HttpResponseMessage ServiceError(string message) {
            return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
        }

        public HttpResponseMessage AccessViolation(string message)
        {
            return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
        }

    }
}