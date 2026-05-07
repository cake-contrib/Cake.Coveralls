using Cake.Common.Diagnostics;
using Cake.Coveralls;
using Cake.Frosting;

namespace Build.Tasks
{
    [TaskName("ReportType-Enums")]
    public sealed class ReportTypeEnumsTask : FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            var ioTypes = new[]
            {
                CoverallsIoReportType.OpenCover,
                CoverallsIoReportType.Cobertura,
            };

            var netTypes = new[]
            {
                CoverallsNetReportType.OpenCover,
                CoverallsNetReportType.DynamicCodeCoverage,
                CoverallsNetReportType.Monocov,
            };

            if (ioTypes.Length != 2)
            {
                throw new System.Exception("CoverallsIoReportType: expected 2 values");
            }

            if (netTypes.Length != 3)
            {
                throw new System.Exception("CoverallsNetReportType: expected 3 values");
            }

            context.Information("ReportType enums OK (Io: {0} values, Net: {1} values)", ioTypes.Length, netTypes.Length);
        }
    }
}
