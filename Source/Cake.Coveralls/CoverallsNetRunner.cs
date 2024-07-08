using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;
using System;
using System.Collections.Generic;

namespace Cake.Coveralls
{
    /// <summary>
    /// The Coveralls.io runner.
    /// </summary>
    public sealed class CoverallsNetRunner : Tool<CoverallsNetSettings>
    {
        private readonly ICakeEnvironment _environment;

        /// <summary>
        /// Initializes a new instance of the <see cref="CoverallsNetRunner" /> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="processRunner">The process runner.</param>
        /// <param name="toolLocator">The tool locator.</param>
        public CoverallsNetRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator toolLocator)
            : base(fileSystem, environment, processRunner, toolLocator)
        {
            _environment = environment;
        }

        /// <summary>
        /// Publish the code coverage report to Coveralls.io using the specified settings.
        /// </summary>
        /// <param name="codeCoverageReportFilePath">The code coverage report file path.</param>
        /// <param name="reportType">The Code Coverage Report Type.</param>
        /// <param name="settings">The settings.</param>
        public void Run(FilePath codeCoverageReportFilePath, CoverallsNetReportType reportType, CoverallsNetSettings settings)
        {
            ArgumentNullException.ThrowIfNull(codeCoverageReportFilePath);
            ArgumentNullException.ThrowIfNull(settings);

            Run(settings, GetArguments(codeCoverageReportFilePath, reportType, settings));
        }

        /// <summary>
        /// Gets the name of the tool.
        /// </summary>
        /// <returns>The name of the tool.</returns>
        protected override string GetToolName()
        {
            return "CoverallsNet";
        }

        /// <summary>
        /// Gets the name of the tool executable.
        /// </summary>
        /// <returns>The tool executable name.</returns>
        protected override IEnumerable<string> GetToolExecutableNames()
        {
            return new[] { "csmacnz.Coveralls.exe", "csmacnz.Coveralls" };
        }

        private static string GetReportType(CoverallsNetReportType reportType) => reportType switch
        {
            CoverallsNetReportType.OpenCover => "--opencover",
            CoverallsNetReportType.DynamicCodeCoverage => "--dynamiccodecoverage",
            CoverallsNetReportType.Monocov => "--monocov",
            _ => throw new NotSupportedException("The provided output is not valid."),
        };

        private ProcessArgumentBuilder GetArguments(FilePath codeCoverageReportFilePath, CoverallsNetReportType reportType, CoverallsNetSettings settings)
        {
            var builder = new ProcessArgumentBuilder();

            builder.Append(GetReportType(reportType));
            builder.Append("--input");
            builder.AppendQuoted(codeCoverageReportFilePath.MakeAbsolute(_environment).FullPath);

            if (settings.OutputFilePath != null)
            {
                builder.Append("--output");
                builder.AppendQuoted(settings.OutputFilePath.MakeAbsolute(_environment).FullPath);
            }

            if (settings.UseRelativePaths)
            {
                builder.Append("--useRelativePaths");
            }

            if (settings.BaseFilePath != null)
            {
                builder.Append("--basePath");
                builder.AppendQuoted(settings.BaseFilePath.MakeAbsolute(_environment).FullPath);
            }

            if (!string.IsNullOrWhiteSpace(settings.RepoToken))
            {
                builder.Append("--repoToken");
                builder.AppendQuotedSecret(settings.RepoToken);
            }

            if (!string.IsNullOrWhiteSpace(settings.RepoTokenVariable))
            {
                builder.Append("--repoTokenVariable");
                builder.AppendQuoted(settings.RepoTokenVariable);
            }

            if (!string.IsNullOrWhiteSpace(settings.CommitId))
            {
                builder.Append("--commitId");
                builder.AppendQuoted(settings.CommitId);
            }

            if (!string.IsNullOrWhiteSpace(settings.CommitBranch))
            {
                builder.Append("--commitBranch");
                builder.AppendQuoted(settings.CommitBranch);
            }

            if (!string.IsNullOrWhiteSpace(settings.CommitAuthor))
            {
                builder.Append("--commitAuthor");
                builder.AppendQuoted(settings.CommitAuthor);
            }

            if (!string.IsNullOrWhiteSpace(settings.CommitEmail))
            {
                builder.Append("--commitEmail");
                builder.AppendQuoted(settings.CommitEmail);
            }

            if (!string.IsNullOrWhiteSpace(settings.CommitMessage))
            {
                builder.Append("--commitMessage");
                builder.AppendQuoted(settings.CommitMessage);
            }

            if (!string.IsNullOrEmpty(settings.JobId))
            {
                builder.Append("--jobId");
                builder.Append(settings.JobId);
            }

            if (!string.IsNullOrWhiteSpace(settings.ServiceName))
            {
                builder.Append("--serviceName");
                builder.AppendQuoted(settings.ServiceName);
            }

            if (settings.TreatUploadErrorsAsWarnings)
            {
                builder.Append("--treatUploadErrorsAsWarnings");
            }

            return builder;
        }
    }
}
