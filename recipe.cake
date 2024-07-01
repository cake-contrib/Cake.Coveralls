#load nuget:?package=Cake.Recipe&version=2.2.1

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

ToolSettings.SetToolSettings(context: Context,
                            testCoverageFilter: "+[*]* -[xunit.*]* -[Cake.Core]* -[Cake.Testing]* -[*.Tests]*",
                            testCoverageExcludeByAttribute: "*.ExcludeFromCodeCoverage*",
                            testCoverageExcludeByFile: "*/*Designer.cs;*/*.g.cs;*/*.g.i.cs");

Build.RunDotNetCore();
