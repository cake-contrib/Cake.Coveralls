using Cake.Common.Diagnostics;
using Cake.Coveralls;
using Cake.Frosting;

namespace Build.Tasks
{
    [TaskName("Settings-CoverallsIo")]
    public sealed class SettingsCoverallsIoTask : FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            var settings = new CoverallsIoSettings
            {
                Debug = true,
                FullSources = true,
                RepoToken = "fake-token",
                ReportType = CoverallsIoReportType.Cobertura,
            };

            if (!settings.Debug)
            {
                throw new System.Exception("Debug round-trip failed");
            }

            if (!settings.FullSources)
            {
                throw new System.Exception("FullSources round-trip failed");
            }

            if (settings.RepoToken != "fake-token")
            {
                throw new System.Exception("RepoToken round-trip failed");
            }

            if (settings.ReportType != CoverallsIoReportType.Cobertura)
            {
                throw new System.Exception("ReportType round-trip failed");
            }

            context.Information("CoverallsIoSettings OK (Debug={0}, FullSources={1}, ReportType={2})",
                settings.Debug, settings.FullSources, settings.ReportType);
        }
    }
}
