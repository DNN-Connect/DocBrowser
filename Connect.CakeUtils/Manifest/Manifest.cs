using System;
using System.Xml;

namespace Connect.CakeUtils.Manifest
{
    public class Manifest : XmlDocument
    {
        public Manifest(Project project) : base()
        {
            Console.WriteLine("Creating Manifest");
            var projectXml = project.Serialize().ToXmlDocument();
            // Set up document
            var rootNode = CreateElement("dotnetnuke");
            AppendChild(rootNode);
            rootNode.AddAttribute("type", "Package");
            rootNode.AddAttribute("version", "5.0");
            var packages = rootNode.AddChildElement("packages");
            var package = packages.AddChildElement("package");
            package.AddAttribute("name", project.dnn.packageName);
            // Set core attributes
            package.SetAttribute("version", project.version);
            package.SetChildElement("friendlyName", project.dnn.friendlyName);
            package.SetChildElement("description", project.description);
            package.SetChildElement("iconFile", project.dnn.iconFile);
            // Add owner
            var owner = package.SetChildElement("owner");
            owner.SetChildElement("name", project.dnn.owner.name);
            owner.SetChildElement("organization", project.dnn.owner.organization);
            owner.SetChildElement("url", project.dnn.owner.url);
            owner.SetChildElement("email", project.dnn.owner.email);
            package.SetChildElement("azureCompatible", project.dnn.module.azureCompatible.ToString().ToLower());
            // core dependency
            var coreRef = string.IsNullOrEmpty(project.dnn.dnnDependency) ? Common.GetCoreDependency(project.dnn.pathsAndFiles.pathToAssemblies) : project.dnn.dnnDependency;
            if (coreRef != "00.00.00")
            {
                var dependencies = package.SetChildElement("dependencies");
                if (dependencies.SelectNodes("dependency[type='CoreVersion']").Count == 0)
                {
                    var coredep = dependencies.AddChildElement("dependency");
                    coredep.SetAttribute("type", "CoreVersion");
                    coredep.InnerText = coreRef;
                }
            }
            package.SetChildElement("license").SetAttribute("src", "License.txt");
            package.SetChildElement("releaseNotes").SetAttribute("src", "ReleaseNotes.txt");

            // Now for the components
            var components = CreateElement("components");

            switch (project.dnn.projectType)
            {
                case "module":
                    package.AddAttribute("type", "Module");
                    components.AppendChild(project.dnn.module.ToXml(components));
                    components.AddScripts(project);
                    components.AddAssemblies(project);
                    components.AddCleanupFiles(project);
                    components.AddResourceComponent(project);
                    package.AppendChild(components);
                    break;
                case "skin":
                    // todo
                    break;
            }
            Console.WriteLine("Finished Creating Manifest");
        }
    }
}
