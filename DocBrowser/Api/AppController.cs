using System.Net;
using System.Net.Http;
using System.Web.Http;
using DotNetNuke.Web.Api;
using Connect.DNN.Modules.DocBrowser.Common;

namespace Connect.DNN.Modules.DocBrowser.Api
{
	public partial class AppController : DocBrowserApiController
	{
		[HttpPost()]
		[DnnModuleAuthorize(AccessLevel = DotNetNuke.Security.SecurityAccessLevel.Host)]
        [ValidateAntiForgeryToken]
		public HttpResponseMessage Reset()
		{
            Connect.DocBrowser.Core.Controllers.AppController.Reset(PortalSettings, ActiveModule.ModuleID);
			return Request.CreateResponse(HttpStatusCode.OK);
		}

	}
}

