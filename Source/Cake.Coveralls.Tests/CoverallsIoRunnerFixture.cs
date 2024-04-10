using Cake.Core.IO;
using Cake.Testing.Fixtures;

namespace Cake.Coveralls.Tests
{
    internal sealed class CoverallsIoRunnerFixture : ToolFixture<CoverallsIoSettings>
    {
        public CoverallsIoRunnerFixture()
             : base("coveralls.net.exe")
        {
            CodeCoverageReportFilePath = FileSystem.GetFile("/temp/coverage.xml").Path.FullPath;
        }

        public FilePath CodeCoverageReportFilePath { get; private set; }

        protected override void RunTool()
        {
            var tool = new CoverallsIoRunner(FileSystem, Environment, ProcessRunner, Tools);
            tool.Run(CodeCoverageReportFilePath, Settings);
        }

        public void WithoutCodeCoverageReportFilePath()
        {
            CodeCoverageReportFilePath = null;
        }
    }
}
