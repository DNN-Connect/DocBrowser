#addin nuget:?package=Newtonsoft.Json&version=9.0.1
#addin "Cake.FileHelpers"
#addin "Cake.Npm"
#reference "BuildSupport/Connect.CakeUtils.dll"

using Connect.CakeUtils;
using Connect.CakeUtils.Compression;
using Connect.CakeUtils.Manifest;

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var packageJson = Context.FileReadText(".\\package.json");
var project = Newtonsoft.Json.JsonConvert.DeserializeObject<Project>(packageJson);
var slnFile ="./Connect.DocBrowser.sln";
var buildSettings = new MSBuildSettings()
.SetConfiguration(configuration)
.UseToolVersion(MSBuildToolVersion.VS2015)
.WithProperty("OutDir", new System.IO.DirectoryInfo(project.dnn.pathsAndFiles.pathToAssemblies).FullName);
var devDnnPath = "D:\\Webroot\\DNNAPI\\";

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

Task("CleanDev")
.Does(() => {
    switch (project.dnn.projectType) {
        case "":
            var moduleFolder = devDnnPath + "DesktopModules\\" + project.dnn.folder.Replace("/", "\\");
            try
            {
                System.IO.Directory.Delete(moduleFolder, true);
            }
            catch (System.Exception)
            {
            }
            System.IO.Directory.CreateDirectory(moduleFolder);
            break;
    }
});

Task("CopyDev")
.IsDependentOn("CleanDev")
.Does(() => {
    switch (project.dnn.projectType) {
        case "":
            var moduleFolder = devDnnPath + "DesktopModules\\" + project.dnn.folder.Replace("/", "\\");
            foreach (var file in Utilities.GetFilesByPatterns(Context, project.dnn.pathsAndFiles.devFolder, project.dnn.pathsAndFiles.releaseFiles, Utilities.ExcludeFunction(project))) {
                Console.WriteLine("Copying {1}", DateTime.Now, file.FullPath);
                var dest = new System.IO.FileInfo(moduleFolder + "\\" + file.FullPath.Substring(new System.IO.DirectoryInfo(project.dnn.pathsAndFiles.devFolder).FullName.Length));
                dest.Directory.Create();
                System.IO.File.Copy(file.FullPath, dest.FullName, true);
            }
            break;
    }
});

Task("AssemblyInfo")
.Does(() => {
    var settings = new GlobberSettings();
    settings.Predicate = Utilities.ExcludeFunction(project);
    var files = GetFiles("./**/AssemblyInfo.cs", settings);
    foreach(var file in files)
    {
        Information("Updating Assembly: {0}", file);
        Utilities.UpdateAssemblyInfo(project, file.FullPath);
    }
});

Task("Build")
.IsDependentOn("AssemblyInfo")
.Does(() => {
    CleanDirectory(project.dnn.pathsAndFiles.pathToAssemblies);
    NuGetRestore(slnFile);
    MSBuild(slnFile, buildSettings);
    NpmRunScript("build");
});

Task("PackageInstall")
.IsDependentOn("Build")
.Does(() => {
    var files = Utilities.GetFilesByPatterns(Context, project.dnn.pathsAndFiles.devFolder, project.dnn.pathsAndFiles.releaseFiles, Utilities.ExcludeFunction(project));
    var resZip = ZipToBytes(project.dnn.pathsAndFiles.devFolder, files);
    Information("Zipped resources file");
    var packageName = project.dnn.pathsAndFiles.zipName + "_" + project.version + "_Install.zip";
    var packagePath = project.dnn.pathsAndFiles.packagesPath + "/" + packageName;
    AddBinaryFileToZip(packagePath, resZip, "resources.zip", false);
    files = Utilities.GetFilesByPatterns(Context, new string[] {
        project.dnn.pathsAndFiles.pathToAssemblies + "/*.dll"
        });
    AddFilesToZip(packagePath, project.dnn.pathsAndFiles.pathToAssemblies, project.dnn.pathsAndFiles.packageAssembliesFolder, files, true);
    files = Utilities.GetFilesByPatterns(Context, new string[] {
        project.dnn.pathsAndFiles.pathToScripts + "/*.SqlDataProvider"
        });
    AddFilesToZip(packagePath, project.dnn.pathsAndFiles.pathToScripts, project.dnn.pathsAndFiles.packageScriptsFolder, files, true);
    files = Utilities.GetFilesByPatterns(Context, new string[] {
        project.dnn.pathsAndFiles.pathToSupplementaryFiles + "/License.txt"
        });
    AddFilesToZip(packagePath, project.dnn.pathsAndFiles.pathToSupplementaryFiles, files, true);
    var releaseNotes = Connect.CakeUtils.Markdown.ToHtml(project.dnn.pathsAndFiles.pathToSupplementaryFiles + "/ReleaseNotes.md");
    if (releaseNotes != "") {
        AddTextFileToZip(packagePath, releaseNotes, "ReleaseNotes.txt", true);
    } else {
        files = Utilities.GetFilesByPatterns(Context, new string[] {
            project.dnn.pathsAndFiles.pathToSupplementaryFiles + "/ReleaseNotes.txt"
        });
        AddFilesToZip(packagePath, project.dnn.pathsAndFiles.pathToSupplementaryFiles, files, true);
    }
    var m = new Manifest(project);
    AddXmlFileToZip(packagePath, m, project.dnn.name + ".dnn", true);
});

Task("Watch")
.IsDependentOn("CopyDev")
.Does(()=>{
    var w = new Watcher(Context, project, new System.IO.DirectoryInfo(project.dnn.pathsAndFiles.devFolder).FullName, devDnnPath);
});

Task("Default")
.IsDependentOn("Watch")
.Does(() => {
});

RunTarget(target);

