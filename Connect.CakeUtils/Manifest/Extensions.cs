using System.Xml;
using static Connect.CakeUtils.Project.DnnProject;

namespace Connect.CakeUtils.Manifest
{
    public static class Extensions
    {
        public static XmlNode ToXml(this DNNMod control, XmlNode parent)
        {
            var newNode = parent.AddChildElement("component").AddAttribute("type", "Module");
            var dtm = newNode.AddChildElement("desktopModule");
            dtm.AddChildElement("moduleName", control.moduleName);
            dtm.AddChildElement("foldername", control.foldername);
            dtm.AddChildElementIfNotNull("businessControllerClass", control.businessControllerClass);
            if (control.supportedFeatures != null && control.supportedFeatures.Length > 0)
            {
                var supfeats = dtm.AddChildElement("supportedFeatures");
                foreach (var sf in control.supportedFeatures)
                {
                    supfeats.AddChildElement("supportedFeature", "").AddAttribute("type", sf.type);
                }
                dtm.AppendChild(supfeats);
            }
            if (control.moduleDefinitions != null && control.moduleDefinitions.Length > 0)
            {
                var mdefs = dtm.AddChildElement("moduleDefinitions");
                foreach (var md in control.moduleDefinitions)
                {
                    mdefs.AppendChild(md.ToXml(mdefs));
                }
                dtm.AppendChild(mdefs);
            }
            return newNode;
        }
        public static XmlNode ToXml(this DNNModDefinition control, XmlNode parent)
        {
            var newNode = parent.AddChildElement("moduleDefinition");
            newNode.AddChildElement("friendlyName", control.friendlyName);
            newNode.AddChildElement("defaultCacheTime", control.defaultCacheTime.ToString());
            if (control.moduleControls != null && control.moduleControls.Length > 0)
            {
                var modcs = newNode.AddChildElement("moduleControls");
                foreach (var mc in control.moduleControls)
                {
                    modcs.AppendChild(mc.ToXml(modcs));
                }
                newNode.AppendChild(modcs);
            }
            return newNode;
        }
        public static XmlNode ToXml(this DNNModControl control, XmlNode parent)
        {
            var newNode = parent.AddChildElement("moduleControl");
            newNode.AddChildElementIfNotNull("controlKey", control.controlKey);
            newNode.AddChildElementIfNotNull("controlSrc", control.controlSrc);
            newNode.AddChildElementIfNotNull("supportsPartialRendering", control.supportsPartialRendering);
            newNode.AddChildElementIfNotNull("controlType", control.controlType);
            newNode.AddChildElementIfNotNull("iconFile", control.iconFile);
            newNode.AddChildElementIfNotNull("helpUrl", control.helpUrl);
            newNode.AddChildElementIfNotNull("viewOrder", control.viewOrder.ToString());
            return newNode;
        }
    }
}
