using System;
using Cake.Core;
using Cake.Testing;
using Xunit;

namespace Cake.Coveralls.Tests
{
    public sealed class CoverallsNetRunnerTests
    {
        public sealed class TheExecutable
        {
            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                // Given
                var fixture = new CoverallsNetRunnerFixture();
                fixture.Settings = null;

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                Assert.IsType<ArgumentNullException>(result);
                Assert.Equal("settings", ((ArgumentNullException)result).ParamName);
            }

            [Fact]
            public void Should_Throw_If_CoverallsNet_Executable_Was_Not_Found()
            {
                // Given
                var fixture = new CoverallsNetRunnerFixture();
                fixture.GivenDefaultToolDoNotExist();

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                Assert.IsType<CakeException>(result);
                Assert.Equal("CoverallsNet: Could not locate executable.", result.Message);
            }

            [Theory]
            [WindowsOnlyInlineData("C:/CoverallsNet/csmacnz.Coveralls.exe", "C:/CoverallsNet/csmacnz.Coveralls.exe")]
            [InlineData("/bin/tools/CoverallsNet/csmacnz.Coveralls.exe", "/bin/tools/CoverallsNet/csmacnz.Coveralls.exe")]
            [InlineData("./tools/CoverallsNet/csmacnz.Coveralls.exe", "/Working/tools/CoverallsNet/csmacnz.Coveralls.exe")]
            public void Should_Use_CoverallsNet_Runner_From_Tool_Path_If_Provided(string toolPath, string expected)
            {
                // Given
                var fixture = new CoverallsNetRunnerFixture { Settings = { ToolPath = toolPath } };
                fixture.GivenSettingsToolPathExist();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal(expected, result.Path.FullPath);
            }

            [Fact]
            public void Should_Find_CoverallsNet_Runner_If_Tool_Path_Not_Provided()
            {
                // Given
                var fixture = new CoverallsNetRunnerFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("/Working/tools/csmacnz.Coveralls.exe", result.Path.FullPath);
            }

            [Fact]
            public void Should_Throw_If_Code_Coverage_File_Path_Is_Null()
            {
                // Given
                var fixture = new CoverallsNetRunnerFixture();
                fixture.WithoutCodeCoverageReportFilePath();

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                Assert.IsType<ArgumentNullException>(result);
                Assert.Equal("codeCoverageReportFilePath", ((ArgumentNullException)result).ParamName);
            }

            [Fact]
            public void Should_Set_Working_Directory()
            {
                // Given
                var fixture = new CoverallsNetRunnerFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("/Working", result.Process.WorkingDirectory.FullPath);
            }

            [Fact]
            public void Should_Throw_If_Process_Was_Not_Started()
            {
                // Given
                var fixture = new CoverallsNetRunnerFixture();
                fixture.GivenProcessCannotStart();

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                Assert.IsType<CakeException>(result);
                Assert.Equal("CoverallsNet: Process was not started.", result.Message);
            }

            [Fact]
            public void Should_Throw_If_Process_Has_A_Non_Zero_Exit_Code()
            {
                // Given
                var fixture = new CoverallsNetRunnerFixture();
                fixture.GivenProcessExitsWithCode(1);

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                Assert.IsType<CakeException>(result);
                Assert.Equal("CoverallsNet: Process returned an error (exit code 1).", result.Message);
            }

            [Theory]
            [WindowsOnlyInlineData("c:/temp/output.xml", "c:/temp/output.xml")]
            [InlineData("/temp/output.xml", "/temp/output.xml")]
            [InlineData("./temp/output.xml", "/Working/temp/output.xml")]
            public void Should_Set_Output_File_Path(string path, string expected)
            {
                // Given
                var fixture = new CoverallsNetRunnerFixture { Settings = { OutputFilePath = path } };

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"--opencover --input \"{fixture.CodeCoverageReportFilePath}\" --output \"{expected}\"", result.Args);
            }

            [Fact]
            public void Should_Set_Use_Relative_Paths()
            {
                // Given
                var fixture = new CoverallsNetRunnerFixture { Settings = { UseRelativePaths = true } };

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"--opencover --input \"{fixture.CodeCoverageReportFilePath}\" --useRelativePaths", result.Args);
            }

            [Theory]
            [WindowsOnlyInlineData("c:/temp", "c:/temp")]
            [InlineData("/temp", "/temp")]
            [InlineData("./temp", "/Working/temp")]
            public void Should_Set_Base_File_Path(string path, string expected)
            {
                // Given
                var fixture = new CoverallsNetRunnerFixture { Settings = { BaseFilePath = path } };

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"--opencover --input \"{fixture.CodeCoverageReportFilePath}\" --basePath \"{expected}\"", result.Args);
            }

            [Fact]
            public void Should_Set_Repo_Token()
            {
                // Given
                var fixture = new CoverallsNetRunnerFixture { Settings = { RepoToken = "abcdef" } };

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"--opencover --input \"{fixture.CodeCoverageReportFilePath}\" --repoToken \"abcdef\"", result.Args);
            }

            [Fact]
            public void Should_Set_Repo_Token_Variable()
            {
                // Given
                var fixture = new CoverallsNetRunnerFixture { Settings = { RepoTokenVariable = "COVERALLS_REPO_TOKEN" } };

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"--opencover --input \"{fixture.CodeCoverageReportFilePath}\" --repoTokenVariable \"COVERALLS_REPO_TOKEN\"", result.Args);
            }

            [Fact]
            public void Should_Set_Commit_Id()
            {
                // Given
                var fixture = new CoverallsNetRunnerFixture { Settings = { CommitId = "123456" } };

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"--opencover --input \"{fixture.CodeCoverageReportFilePath}\" --commitId \"123456\"", result.Args);
            }

            [Fact]
            public void Should_Set_Commit_Branch()
            {
                // Given
                var fixture = new CoverallsNetRunnerFixture { Settings = { CommitBranch = "master" } };

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"--opencover --input \"{fixture.CodeCoverageReportFilePath}\" --commitBranch \"master\"", result.Args);
            }

            [Fact]
            public void Should_Set_Commit_Author()
            {
                // Given
                var fixture = new CoverallsNetRunnerFixture { Settings = { CommitAuthor = "gep13" } };

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"--opencover --input \"{fixture.CodeCoverageReportFilePath}\" --commitAuthor \"gep13\"", result.Args);
            }

            [Fact]
            public void Should_Set_Commit_Email()
            {
                // Given
                var fixture = new CoverallsNetRunnerFixture { Settings = { CommitEmail = "gep13@gep13.co.uk" } };

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"--opencover --input \"{fixture.CodeCoverageReportFilePath}\" --commitEmail \"gep13@gep13.co.uk\"", result.Args);
            }

            [Fact]
            public void Should_Set_Commit_Message()
            {
                // Given
                var fixture = new CoverallsNetRunnerFixture { Settings = { CommitMessage = "boom!" } };

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"--opencover --input \"{fixture.CodeCoverageReportFilePath}\" --commitMessage \"boom!\"", result.Args);
            }

            [Fact]
            public void Should_Set_Job_Id()
            {
                // Given
                var fixture = new CoverallsNetRunnerFixture { Settings = { JobId = "123456" } };

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"--opencover --input \"{fixture.CodeCoverageReportFilePath}\" --jobId 123456", result.Args);
            }

            [Fact]
            public void Should_Set_Service_Name()
            {
                // Given
                var fixture = new CoverallsNetRunnerFixture { Settings = { ServiceName = "coveralls.net" } };

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"--opencover --input \"{fixture.CodeCoverageReportFilePath}\" --serviceName \"coveralls.net\"", result.Args);
            }

            [Fact]
            public void Should_Set_Treat_Upload_Errors_As_Warnings()
            {
                // Given
                var fixture = new CoverallsNetRunnerFixture { Settings = { TreatUploadErrorsAsWarnings = true } };

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal($"--opencover --input \"{fixture.CodeCoverageReportFilePath}\" --treatUploadErrorsAsWarnings", result.Args);
            }
        }
    }
}