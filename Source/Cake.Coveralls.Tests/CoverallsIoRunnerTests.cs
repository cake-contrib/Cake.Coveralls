using System;
using Cake.Core;
using Cake.Testing;
using Xunit;

namespace Cake.Coveralls.Tests
{
    public sealed class CoverallsIoRunnerTests
    {
        public sealed class TheExecutable
        {
            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                // Given
                var fixture = new CoverallsIoRunnerFixture();
                fixture.Settings = null;

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                Assert.IsType<ArgumentNullException>(result);
                Assert.Equal("settings", ((ArgumentNullException)result).ParamName);
            }

            [Fact]
            public void Should_Throw_If_CoverallsIo_Executable_Was_Not_Found()
            {
                // Given
                var fixture = new CoverallsIoRunnerFixture();
                fixture.GivenDefaultToolDoNotExist();

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                Assert.IsType<CakeException>(result);
                Assert.Equal("CoverallsIo: Could not locate executable.", result.Message);
            }

            [Theory]
            [InlineData("/bin/tools/CoverallsIo/coveralls.net.exe", "/bin/tools/CoverallsIo/coveralls.net.exe")]
            [InlineData("./tools/CoverallsIo/coveralls.net.exe", "/Working/tools/CoverallsIo/coveralls.net.exe")]
            public void Should_Use_CoverallsIo_Runner_From_Tool_Path_If_Provided(string toolPath, string expected)
            {
                // Given
                var fixture = new CoverallsIoRunnerFixture { Settings = { ToolPath = toolPath } };
                fixture.GivenSettingsToolPathExist();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal(expected, result.Path.FullPath);
            }

            [Theory]
            [InlineData("C:/CoverallsIo/coveralls.net.exe", "C:/CoverallsIo/coveralls.net.exe")]
            public void Should_Use_CoverallsIo_Runner_From_Tool_Path_If_Provided_On_Windows(string toolPath, string expected)
            {
                // Given
                var fixture = new CoverallsIoRunnerFixture { Settings = { ToolPath = toolPath } };
                fixture.GivenSettingsToolPathExist();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal(expected, result.Path.FullPath);
            }

            [Fact]
            public void Should_Find_CoverallsIo_Runner_If_Tool_Path_Not_Provided()
            {
                // Given
                var fixture = new CoverallsIoRunnerFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("/Working/tools/coveralls.net.exe", result.Path.FullPath);
            }

            [Fact]
            public void Should_Throw_If_Code_Coverage_File_Path_Is_Null()
            {
                // Given
                var fixture = new CoverallsIoRunnerFixture();
                fixture.CodeCoverageReportFilePath = null;

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
                var fixture = new CoverallsIoRunnerFixture();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("/Working", result.Process.WorkingDirectory.FullPath);
            }

            [Fact]
            public void Should_Throw_If_Process_Was_Not_Started()
            {
                // Given
                var fixture = new CoverallsIoRunnerFixture();
                fixture.GivenProcessCannotStart();

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                Assert.IsType<CakeException>(result);
                Assert.Equal("CoverallsIo: Process was not started.", result.Message);
            }

            [Fact]
            public void Should_Throw_If_Process_Has_A_Non_Zero_Exit_Code()
            {
                // Given
                var fixture = new CoverallsIoRunnerFixture();
                fixture.GivenProcessExitsWithCode(1);

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                Assert.IsType<CakeException>(result);
                Assert.Equal("CoverallsIo: Process returned an error (exit code 1).", result.Message);
            }

            [Fact]
            public void Should_Set_Debug()
            {
                // Given
                var fixture = new CoverallsIoRunnerFixture { Settings = { Debug = true } };

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("--opencover \"c:/temp/coverage.xml\" --debug", result.Args);
            }

            [Fact]
            public void Should_Set_Full_Sources()
            {
                // Given
                var fixture = new CoverallsIoRunnerFixture { Settings = { FullSources = true } };

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("--opencover \"c:/temp/coverage.xml\" --full-sources", result.Args);
            }

            [Fact]
            public void Should_Set_RepoToken()
            {
                // Given
                var fixture = new CoverallsIoRunnerFixture { Settings = { RepoToken = "abcdef" } };

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("--opencover \"c:/temp/coverage.xml\" --repo-token \"abcdef\"", result.Args);
            }
        }
    }
}