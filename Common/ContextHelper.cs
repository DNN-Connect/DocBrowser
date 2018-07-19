using System;
using System.Web.Mvc;
using DotNetNuke.Common;
using DotNetNuke.Web.Client.ClientResourceManagement;
using DotNetNuke.Web.Mvc.Framework.Controllers;
using DotNetNuke.Web.Api;
using DotNetNuke.Entities.Modules;

namespace Connect.DNN.Modules.DocBrowser.Common
{
    public class ContextHelper
    {
        public ModuleInfo ModuleContext { get; set; }
        public System.Web.UI.Page Page { get; set; }

        public ContextHelper(ViewContext viewContext)
        {
            Requires.NotNull("viewContext", viewContext);

            var controller = viewContext.Controller as IDnnController;

            if (controller == null)
            {
                throw new InvalidOperationException("The DnnUrlHelper class can only be used in Views that inherit from DnnWebViewPage");
            }

            ModuleContext = controller.ModuleContext.Configuration;
            Page = controller.DnnPage;
        }

        public ContextHelper(DnnController context)
        {
            Requires.NotNull("context", context);
            ModuleContext = context.ModuleContext.Configuration;
            Page = context.DnnPage;
        }

        public ContextHelper(DnnApiController context)
        {
            Requires.NotNull("context", context);
            ModuleContext = context.ActiveModule;
        }

        private ContextSecurity _security;
        public ContextSecurity Security
        {
            get { return _security ?? (_security = new ContextSecurity(ModuleContext)); }
        }

        private ModuleSettings _settings;
        public ModuleSettings Settings
        {
            get { return _settings ?? (_settings = ModuleSettings.GetSettings(ModuleContext)); }
        }

        public void ThrowAccessViolation()
        {
            throw new Exception("You do not have adequate permissions to view this resource. Please check your login status.");
        }

        #region Css Files
        public void AddCss(string cssFile, string name, string version)
        {
            ClientResourceManager.RegisterStyleSheet(Page, string.Format("~/DesktopModules/MVC/Connect/DocBrowser/css/{0}", cssFile), 70, "", name, version);
        }
        public void AddCss(string cssFile)
        {
            ClientResourceManager.RegisterStyleSheet(Page, string.Format("~/DesktopModules/MVC/Connect/DocBrowser/css/{0}", cssFile));
        }

        #endregion

        #region Js Files
        public void AddJqueryUi()
        {
            DotNetNuke.Framework.JavaScriptLibraries.JavaScript.RequestRegistration(DotNetNuke.Framework.JavaScriptLibraries.CommonJs.jQueryUI);
        }
        public void AddScript(string scriptName, string name, string version)
        {
            ClientResourceManager.RegisterScript(Page, string.Format("~/DesktopModules/MVC/Connect/DocBrowser/js/{0}", scriptName), 70, "", name, version);
        }
        public void AddScript(string scriptName)
        {
            ClientResourceManager.RegisterScript(Page, string.Format("~/DesktopModules/MVC/Connect/DocBrowser/js/{0}", scriptName));
        }
        public void AddModuleScript()
        {
            DotNetNuke.Framework.ServicesFramework.Instance.RequestAjaxScriptSupport();

            AddScript("connect-docbrowser.js");
        }
        #endregion

    }
}