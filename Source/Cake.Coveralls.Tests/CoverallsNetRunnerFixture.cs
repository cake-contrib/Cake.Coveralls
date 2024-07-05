using Cake.Core.IO;
using Cake.Testing.Fixtures;

namespace Cake.Coveralls.Tests
{
    internal sealed class CoverallsNetRunnerFixture : ToolFixture<CoverallsNetSettings>
    {
        public CoverallsNetRunnerFixture()
             : base("csmacnz.Coveralls.exe")
        {
            CodeCoverageReportFilePath = FileSystem.GetFile("/temp/coverage.xml").Path.FullPath;
            ReportType = CoverallsNetReportType.OpenCover;
        }

        public FilePath CodeCoverageReportFilePath { get; private set; }

        public CoverallsNetReportType ReportType { get; }

        protected override void RunTool()
        {
            var tool = new CoverallsNetRunner(FileSystem, Environment, ProcessRunner, Tools);
            tool.Run(CodeCoverageReportFilePath, ReportType, Settings);
        }

        public void WithoutCodeCoverageReportFilePath()
        {
            CodeCoverageReportFilePath = null;
        }
    }
}