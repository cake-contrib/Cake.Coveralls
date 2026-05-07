using Cake.Common.Diagnostics;
using Cake.Common.IO;
using Cake.Coveralls;
using Cake.Frosting;

namespace Build.Tasks
{
    [TaskName("Settings-CoverallsNet")]
    public sealed class SettingsCoverallsNetTask : FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            var settings = new CoverallsNetSettings
            {
                OutputFilePath = context.File("./coverage.xml"),
                UseRelativePaths = true,
                BaseFilePath = context.Directory("./Source"),
                RepoToken = "fake-token",
                RepoTokenVariable = "COVERALLS_REPO_TOKEN",
                CommitId = "abc123",
                CommitBranch = "develop",
                CommitAuthor = "Cake",
                CommitEmail = "cake@example.com",
                CommitMessage = "exercise script",
                JobId = "42",
                ServiceName = "coveralls.net",
                TreatUploadErrorsAsWarnings = true,
            };

            if (settings.OutputFilePath == null)
            {
                throw new System.Exception("OutputFilePath round-trip failed");
            }

            if (!settings.UseRelativePaths)
            {
                throw new System.Exception("UseRelativePaths round-trip failed");
            }

            if (settings.BaseFilePath == null)
            {
                throw new System.Exception("BaseFilePath round-trip failed");
            }

            if (settings.RepoToken != "fake-token")
            {
                throw new System.Exception("RepoToken round-trip failed");
            }

            if (settings.RepoTokenVariable != "COVERALLS_REPO_TOKEN")
            {
                throw new System.Exception("RepoTokenVariable round-trip failed");
            }

            if (settings.CommitId != "abc123")
            {
                throw new System.Exception("CommitId round-trip failed");
            }

            if (settings.CommitBranch != "develop")
            {
                throw new System.Exception("CommitBranch round-trip failed");
            }

            if (settings.CommitAuthor != "Cake")
            {
                throw new System.Exception("CommitAuthor round-trip failed");
            }

            if (settings.CommitEmail != "cake@example.com")
            {
                throw new System.Exception("CommitEmail round-trip failed");
            }

            if (settings.CommitMessage != "exercise script")
            {
                throw new System.Exception("CommitMessage round-trip failed");
            }

            if (settings.JobId != "42")
            {
                throw new System.Exception("JobId round-trip failed");
            }

            if (settings.ServiceName != "coveralls.net")
            {
                throw new System.Exception("ServiceName round-trip failed");
            }

            if (!settings.TreatUploadErrorsAsWarnings)
            {
                throw new System.Exception("TreatUploadErrorsAsWarnings round-trip failed");
            }

            context.Information("CoverallsNetSettings OK (CommitId={0}, JobId={1}, ServiceName={2})",
                settings.CommitId, settings.JobId, settings.ServiceName);
        }
    }
}
