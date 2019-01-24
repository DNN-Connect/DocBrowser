using System.Collections.Generic;
using System.Reflection;

namespace Connect.CakeUtils.Manifest
{
    public static class Common
    {
        public static Dictionary<string, System.Version> GetReferences(string assembly)
        {
            var res = new Dictionary<string, System.Version>();
            var ass = Assembly.LoadFrom(assembly);
            foreach (var an in ass.GetReferencedAssemblies())
            {
                res.Add(an.Name, an.Version);
            }
            return res;
        }

        public static string GetLastFolderName(string folder)
        {
            if (folder.IndexOf('/') < 0)
            {
                return folder + "/";
            }

            return folder.Substring(folder.LastIndexOf('/') + 1) + "/";
        }

        public static string GetCoreDependency(string assemblyPath)
        {
            var res = new System.Version(0, 0, 0, 0);
            foreach (var f in System.IO.Directory.GetFiles(assemblyPath, "*.dll"))
            {
                var refs = Common.GetReferences(f);
                if (refs.ContainsKey("DotNetNuke"))
                {
                    if (res.CompareTo(refs["DotNetNuke"]) < 0)
                    {
                        res = refs["DotNetNuke"];
                    }
                }
            }
            return res.ToNormalizedVersion();
        }
    }
}
