using DotNetNuke.Web.Api;

namespace Connect.DNN.Modules.DocBrowser.Api
{
    public class RouteMapper : IServiceRouteMapper
    {
        public void RegisterRoutes(IMapRoute mapRouteManager)
        {
            mapRouteManager.MapHttpRoute("Connect/DocBrowser", "DocBrowserMap1", "{controller}/{action}", null, null, new[] { "Connect.DNN.Modules.DocBrowser.Api" });
            mapRouteManager.MapHttpRoute("Connect/DocBrowser", "DocBrowserMap2", "{controller}/{action}/{id}", null, new { id = "-?\\d+" }, new[] { "Connect.DNN.Modules.DocBrowser.Api" });
        }
    }
}