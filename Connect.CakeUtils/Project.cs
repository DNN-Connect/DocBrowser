namespace Connect.CakeUtils
{
    public class Project
    {
        public string name { get; set; }
        public string version { get; set; }
        public string description { get; set; }
        public DnnProject dnn { get; set; }
        public class DnnProject
        {
            public string projectType { get; set; }
            public string dnnDependency { get; set; }
            public string name { get; set; }
            public string friendlyName { get; set; }
            public string packageName { get; set; }
            public string folder { get; set; }
            public string iconFile { get; set; }
            public Owner owner { get; set; }
            public class Owner
            {
                public string name { get; set; }
                public string organization { get; set; }
                public string url { get; set; }
                public string email { get; set; }
            }
            public DNNMod module { get; set; }
            public DNNPathsAndFiles pathsAndFiles { get; set; }
            public class DNNMod
            {
                public string azureCompatible { get; set; }
                public string moduleName { get; set; }
                public string foldername { get; set; }
                public string businessControllerClass { get; set; }
                public DNNModSupportedFeature[] supportedFeatures { get; set; }
                public DNNModDefinition[] moduleDefinitions { get; set; }
            }
            public class DNNModSupportedFeature
            {
                public string type { get; set; }
            }
            public class DNNModDefinition
            {
                public string friendlyName { get; set; }
                public int defaultCacheTime { get; set; } = -1;
                public DNNModControl[] moduleControls { get; set; }
            }
            public class DNNModControl
            {
                public string controlKey { get; set; }
                public string controlSrc { get; set; }
                public string supportsPartialRendering { get; set; }
                public string controlType { get; set; }
                public string iconFile { get; set; }
                public string helpUrl { get; set; }
                public int viewOrder { get; set; } = 0;
            }
            public class DNNPathsAndFiles
            {
                public string devFolder { get; set; }
                public string packagesPath { get; set; } = "./Releases";
                public string pathToScripts { get; set; } = "./Scripts";
                public string pathToSupplementaryFiles { get; set; } = "./Installation";
                public string pathToAssemblies { get; set; }
                public string[] assemblies { get; set; }
                public string zipName { get; set; }
                public string[] excludeFilter { get; set; }
                public string[] releaseFiles { get; set; }
                public string packageAssembliesFolder { get; set; } = "bin";
                public string packageScriptsFolder { get; set; } = "scripts";
            }
        }
    }
}
