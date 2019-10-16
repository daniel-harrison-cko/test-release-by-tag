#tool nuget:?package=NUnit.ConsoleRunner&version=3.4.0
#tool "nuget:?package=GitVersion.CommandLine&version=5.0.1"

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

Task("Build")
    .Does(() =>
{
    var buildSettings = new DotNetCoreBuildSettings
    {
        Configuration = configuration
    };

    DotNetCoreBuild("TestReleaseByTag.sln", buildSettings);
});

Task("Deploy")
    .IsDependentOn("Build")
    .Does(() =>
{
    var version = GitVersion();
    Information($"##teamcity[buildNumber '{version.FullSemVer}']");
});

Task("Default")
    .IsDependentOn("Build");

RunTarget(target);
