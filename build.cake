// Tooling
#tool nuget:?package=Cake.Common&version=1.4.0
#tool nuget:?package=Cake.DotNetTool.Module&version=0.4.0

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
  .Does(() =>
{
  CleanDirectory("./artifacts");
  DotNetClean("./MySolution.sln");
});

Task("Restore")
  .IsDependentOn("Clean")
  .Does(() =>
{
  DotNetRestore("./MySolution.sln");
});

Task("Build")
  .IsDependentOn("Restore")
  .Does(() =>
{
  DotNetBuild("./MySolution.sln", new DotNetBuildSettings {
    Configuration = "Release"
  });
});

Task("Test")
  .IsDependentOn("Build")
  .Does(() =>
{
  Information("Running NUnit tests via Cake...");
  var projects = GetFiles("./Tests/**/.*.csproj");
  foreach(var project in projects) {
    DotNetTest(project.FullPath, new DotNetTestSettings {
      Configuration = "Release",
      NoBuild = true,
      Logger = "trx",
      Collect = new [] { "XPlat Code Coverage" },
      ResultsDirectory = "./artifacts/TestResults"
    });
  }
});

Task("Package")
  .IsDependentOn("Test")
  .Does(() =>
{
  DotNetPublish("./Api/Api.csproj", new DotNetPublishSettings {
    Configuration = "Release",
    OutputDirectory = "./artifacts/publish"
  });
});

Task("Default")
  .IsDependentOn("Package");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
