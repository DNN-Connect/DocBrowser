using Cake.Core;
using Cake.Core.IO;
using System;
using System.IO;

namespace Connect.CakeUtils
{
    public class Watcher
    {
        private FilePathCollection validFiles { get; set; }
        private ICakeContext Context { get; set; }
        private Project Project { get; set; }
        private string RootPath { get; set; }
        private int RootPathLength { get; set; }
        private string DestinationPath { get; set; }
        private string DllPath { get; set; }

        public Watcher(ICakeContext context, Project project, string folderPath, string destinationPath)
        {
            Context = context;
            Project = project;
            RootPath = folderPath.EnsureEndsWith("\\");
            RootPathLength = RootPath.Length;
            DestinationPath = destinationPath.EnsureEndsWith("\\");
            DllPath = DestinationPath + "bin\\";
            if (project.dnn.projectType == "module")
            {
                DestinationPath = DestinationPath + "DesktopModules\\" + project.dnn.folder.Replace("/", "\\").EnsureEndsWith("\\");
            }

            FileSystemWatcher watcher = new FileSystemWatcher();
            // Watch for all events of all files
            watcher.Path = folderPath;
            watcher.IncludeSubdirectories = true;
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
               | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Filter = "*.*";

            // Add event handlers.
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);

            RefreshValidFiles();

            // Begin watching.
            watcher.EnableRaisingEvents = true;

            while (true)
            {
                ;
            }
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            if (IsValidFile(e.FullPath))
            {
                switch (e.ChangeType)
                {
                    case WatcherChangeTypes.Changed:
                    case WatcherChangeTypes.Created:
                        CopyFile(e.FullPath);
                        break;
                    case WatcherChangeTypes.Deleted:
                        DeleteFile(e.FullPath);
                        break;
                }
            }
        }

        private void OnRenamed(object source, RenamedEventArgs e)
        {
            if (IsValidFile(e.FullPath))
            {
                DeleteFile(e.OldFullPath);
                CopyFile(e.FullPath);
            }
        }

        private void CopyFile(string filePath)
        {
            try
            {
                File.Copy(filePath, GetDestinationPath(filePath), true);
            }
            catch (Exception)
            {
                Console.WriteLine("Coudn't copy {0} to {1}", filePath, GetDestinationPath(filePath));
            }
        }

        private void DeleteFile(string filePath)
        {
            try
            {
                File.Delete(GetDestinationPath(filePath));
            }
            catch (Exception)
            {
                Console.WriteLine("Coudn't delete {0}", GetDestinationPath(filePath));
            }
        }

        private string GetDestinationPath(string filePath)
        {
            if (System.IO.Path.GetExtension(filePath).ToLower() == ".dll")
            {
                return DllPath + System.IO.Path.GetFileName(filePath);
            }
            else
            {
                return DestinationPath + GetRelativePath(filePath);
            }
        }

        private string GetRelativePath(string filePath)
        {
            return filePath.Substring(RootPathLength);
        }

        private void RefreshValidFiles()
        {
            validFiles = Utilities.GetFilesByPatterns(Context, Project.dnn.pathsAndFiles.releaseFiles, Utilities.ExcludeFunction(Project));
            if (Project.dnn.pathsAndFiles.assemblies != null)
            {
                foreach (var a in Project.dnn.pathsAndFiles.assemblies)
                {
                    var path = System.IO.Path.Combine(Project.dnn.pathsAndFiles.pathToAssemblies, a);
                    validFiles.Add(new FilePath(new FileInfo(path).FullName));
                }
            }
        }

        private bool IsValidFile(string fileName)
        {
            if (validFiles.Contains(fileName))
            {
                return true;
            }
            return false;
        }
    }
}
