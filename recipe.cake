#load nuget:?package=Cake.Recipe&version=3.1.1

Environment.SetVariableNames();

BuildParameters.SetParameters(context: Context,
                            buildSystem: BuildSystem,
                            sourceDirectoryPath: "./Source",
                            title: "Cake.Coveralls",
                            repositoryOwner: "cake-contrib",
                            repositoryName: "Cake.Coveralls",
                            appVeyorAccountName: "cakecontrib",
                            shouldRunCodecov: false,
                            shouldPostToGitter: false,
                            shouldRunDotNetCorePack: true,
                            preferredBuildProviderType: BuildProviderType.GitHubActions,
                            shouldGenerateDocumentation: false);

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolPreprocessorDirectives(
                            gitReleaseManagerGlobalTool: "#tool dotnet:?package=GitReleaseManager.Tool&version=0.17.0");

ToolSettings.SetToolSettings(context: Context,
                            testCoverageFilter: "+[*]* -[xunit.*]* -[Cake.Core]* -[Cake.Testing]* -[*.Tests]*",
                            testCoverageExcludeByAttribute: "*.ExcludeFromCodeCoverage*",
                            testCoverageExcludeByFile: "*/*Designer.cs;*/*.g.cs;*/*.g.i.cs");

Build.RunDotNetCore();
