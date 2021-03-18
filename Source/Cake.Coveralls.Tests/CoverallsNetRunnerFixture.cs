using Cake.Core.IO;
using Cake.Testing.Fixtures;

namespace Cake.Coveralls.Tests
{
    internal sealed class CoverallsNetRunnerFixture : ToolFixture<CoverallsNetSettings>
    {
        public CoverallsNetRunnerFixture()
             : base("csmacnz.Coveralls.exe")
        {
            CodeCoverageReportFilePath = "c:/temp/coverage.xml";
            ReportType = CoverallsNetReportType.OpenCover;
        }

        public FilePath CodeCoverageReportFilePath { get; set; }

        public CoverallsNetReportType ReportType { get; set; }

        protected override void RunTool()
        {
            var tool = new CoverallsNetRunner(FileSystem, Environment, ProcessRunner, Tools);
            tool.Run(CodeCoverageReportFilePath, ReportType, Settings);
        }
    }
}