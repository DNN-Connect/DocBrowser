#load "_BuildSupport/Utilities.cake"
#load "_BuildSupport/Types.cake"
#addin nuget:?package=Newtonsoft.Json&version=9.0.1
#addin "Cake.FileHelpers"
#reference "D:\Documents\Visual Studio\Projects\Bring2mind.Build\bin\Debug\netstandard2.0\Bring2mind.Build.dll"

using Bring2mind.Build;

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");


var json = Context.FileReadText(".\\package.json");
var project = Newtonsoft.Json.JsonConvert.DeserializeObject<Project>(json);

var slnFile ="./Connect.DocBrowser.sln";
var buildSettings = new MSBuildSettings()
.SetConfiguration(configuration)
.UseToolVersion(MSBuildToolVersion.VS2015)
.WithProperty("OutDir", project.dnn.pathToAssemblies);

Func<IFileSystemInfo, bool> exclude_paths =
     fileSystemInfo => {
         //Information(fileSystemInfo.Path);
         var crt = new System.IO.DirectoryInfo(".");
         var rel = fileSystemInfo.Path.FullPath.Substring(crt.FullName.Length + 1);
         if (rel.IndexOf('/') > -1) {
            rel = rel.Substring(0, rel.IndexOf('/'));
         }
         return !project.dnn.excludeFilter.Contains(rel);
     };


///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(ctx =>
{
   // Executed BEFORE the first task.
   Information("Running tasks...");
});

Teardown(ctx =>
{
   // Executed AFTER the last task.
   Information("Finished running tasks.");
});

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

Task("Restore")
.Does(() => {
    NuGetRestore(slnFile);
});



Task("Clean")
    .Does(() =>
{
    CleanDirectory(project.dnn.pathToAssemblies);
});

Task("AssemblyInfo")
.Does(() => {
     var files = GetFiles("./**/AssemblyInfo.cs", exclude_paths);
 foreach(var file in files)
 {
     Information("File: {0}", file);
     var ai = new Bring2mind.Build.AssemblyInfo(file.FullPath);
     ai.SetProperty("AssemblyVersion", project.version);
     ai.SetProperty("AssemblyFileVersion", project.version);
     ai.SetProperty("AssemblyTitle", project.name);
     ai.SetProperty("AssemblyDescription", project.description);
     ai.SetProperty("AssemblyCompany", project.dnn.owner.organization);
     ai.SetProperty("AssemblyCopyright", string.Format("Copyright {0} by {1}", System.DateTime.Now.Year, project.dnn.owner.organization));
     ai.Write();
 }
});

Task("Build")
//.IsDependentOn("Restore")
.IsDependentOn("Clean")
.IsDependentOn("AssemblyInfo")
.Does(() => {
    MSBuild(slnFile, buildSettings);
});

Task("Manifest")
.IsDependentOn("Build")
.Does(() => {
    var m = GetManifest(project);
    m.Save(project.dnn.name + ".dnn");
});

Task("PackageInstall")
.IsDependentOn("Manifest")
.Does(() => {
 var files = GetFilesByPatterns(Context, new string[] {
     "App_LocalResources/*.resx"
     , "**/*.ascx",
            "Views/**/*.cshtml",
            "fonts/*.*",
            "**/*.html",
            "**/*.png",
            "**/*.gif",
            "**/*.txt",
            "*.dnn"
     }, exclude_paths);
 var packageName = project.dnn.zipName + "_" + project.version + ".zip";
 Zip(".", project.dnn.packagesPath + "/" + packageName, files);
});

Task("Default")
.IsDependentOn("PackageInstall")
.Does(() => {
    Information(project.name);
});

RunTarget(target);

