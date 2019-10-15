#tool nuget:?package=NUnit.ConsoleRunner&version=3.4.0

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

Task("Default")
    .IsDependentOn("Build");

RunTarget(target);
