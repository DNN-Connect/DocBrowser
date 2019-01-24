using Cake.Common.IO;
using Cake.Core;
using Cake.Core.IO;
using System;

namespace Connect.CakeUtils
{
    public class Utilities
    {
        public static void UpdateAssemblyInfo(Project project, string filePath)
        {
            var ai = new AssemblyInfo(filePath);
            ai.SetProperty("AssemblyVersion", project.version);
            ai.SetProperty("AssemblyFileVersion", project.version);
            ai.SetProperty("AssemblyTitle", project.name);
            ai.SetProperty("AssemblyDescription", project.description);
            ai.SetProperty("AssemblyCompany", project.dnn.owner.organization);
            ai.SetProperty("AssemblyCopyright", string.Format("Copyright {0} by {1}", System.DateTime.Now.Year, project.dnn.owner.organization));
            ai.Write();
        }

        public static FilePathCollection GetFilesByPatterns(ICakeContext context, string[] patterns)
        {
            FilePathCollection res = context.GetFiles(patterns[0]);
            for (var i = 1; i < patterns.Length - 1; i++)
            {
                res += context.GetFiles(patterns[i]);
            }
            return res;
        }

        public static FilePathCollection GetFilesByPatterns(ICakeContext context, string[] patterns, Func<IDirectory, bool> predicate)
        {
            var settings = new GlobberSettings();
            settings.Predicate = predicate;
            settings.IsCaseSensitive = false;
            FilePathCollection res = context.GetFiles(patterns[0], settings);
            for (var i = 1; i < patterns.Length - 1; i++)
            {
                res += context.GetFiles(patterns[i], settings);
            }
            return res;
        }

        public static FilePathCollection GetFilesByPatterns(ICakeContext context, string root, string[] patterns)
        {
            root = root.EnsureEndsWith("/");
            FilePathCollection res = context.GetFiles(patterns[0]);
            for (var i = 1; i < patterns.Length - 1; i++)
            {
                res += context.GetFiles(root + patterns[i]);
            }
            return res;
        }

        public static FilePathCollection GetFilesByPatterns(ICakeContext context, string root,  string[] patterns, Func<IDirectory, bool> predicate)
        {
            root = root.EnsureEndsWith("/");
            var settings = new GlobberSettings();
            settings.Predicate = predicate;
            settings.IsCaseSensitive = false;
            FilePathCollection res = context.GetFiles(patterns[0], settings);
            for (var i = 1; i < patterns.Length - 1; i++)
            {
                res += context.GetFiles(root + patterns[i], settings);
            }
            return res;
        }

        public static Func<IFileSystemInfo, bool> ExcludeFunction(Project project)
        {
            return fileSystemInfo => {
                var crt = new System.IO.DirectoryInfo(".");
                var rel = fileSystemInfo.Path.FullPath.Substring(crt.FullName.Length + 1);
                foreach (var ef in project.dnn.pathsAndFiles.excludeFilter)
                {
                    if (rel.StartsWith(ef)) return false;
                }
                return true;
            };
        }
    }
}
