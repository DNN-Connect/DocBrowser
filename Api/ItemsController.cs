using System.Net;
using System.Net.Http;
using System.Web.Http;
using DotNetNuke.Web.Api;
using Connect.DNN.Modules.DocBrowser.Common;
using System.IO;
using System.Net.Http.Headers;

namespace Connect.DNN.Modules.DocBrowser.Api
{
    public partial class ItemsController : DocBrowserApiController
    {
        [HttpGet()]
        [DnnModuleAuthorize(AccessLevel = DotNetNuke.Security.SecurityAccessLevel.View)]
        public HttpResponseMessage Topics(string locale, string version, int edition)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Connect.DocBrowser.Core.Data.Sprocs.GetTopicList(ActiveModule.ModuleID, locale, version, edition));
        }
        [HttpGet()]
        [DnnModuleAuthorize(AccessLevel = DotNetNuke.Security.SecurityAccessLevel.View)]
        public HttpResponseMessage Menu(string locale)
        {
            var rootDir = Path.Combine(PortalSettings.HomeDirectoryMapPath, "Docs\\" + ActiveModule.ModuleID.ToString());
            var fileName = "menu";
            fileName += locale != "en" ? "-" + locale : "";
            fileName += ".json";
            fileName = Path.Combine(rootDir, fileName);
            if (!File.Exists(fileName))
            {
                fileName = Path.Combine(rootDir, "menu.json");
            }
            HttpResponseMessage res = new HttpResponseMessage(HttpStatusCode.OK);
            var dataBytes = File.ReadAllBytes(fileName);
            var dataStream = new MemoryStream(dataBytes);
            res.Content = new StreamContent(dataStream);
            res.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            res.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            res.Content.Headers.ContentDisposition.FileName = "menu.json";
            return res;
        }
        [HttpGet()]
        [DnnModuleAuthorize(AccessLevel = DotNetNuke.Security.SecurityAccessLevel.View)]
        public HttpResponseMessage Topic(string locale, string version, int edition, string topic)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Connect.DocBrowser.Core.Data.Sprocs.GetTopic(ActiveModule.ModuleID, locale, version, edition, topic));
        }
    }
}
