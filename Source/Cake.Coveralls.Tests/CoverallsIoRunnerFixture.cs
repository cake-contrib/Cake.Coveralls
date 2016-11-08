using Cake.Core.IO;
using Cake.Testing.Fixtures;

namespace Cake.Coveralls.Tests
{
    internal sealed class CoverallsIoRunnerFixture : ToolFixture<CoverallsIoSettings>
    {
        public CoverallsIoRunnerFixture()
             : base("coveralls.net.exe")
        {
            CodeCoverageReportFilePath = "c:/temp/coverage.xml";
        }

        public FilePath CodeCoverageReportFilePath { get; set; }

        protected override void RunTool()
        {
            var tool = new CoverallsIoRunner(FileSystem, Environment, ProcessRunner, Tools);
            tool.Run(CodeCoverageReportFilePath, Settings);
        }
    }
}