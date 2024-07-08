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
    public sealed class CoverallsIoRunner : Tool<CoverallsIoSettings>
    {
        private readonly ICakeEnvironment _environment;

        /// <summary>
        /// Initializes a new instance of the <see cref="CoverallsIoRunner" /> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="processRunner">The process runner.</param>
        /// <param name="toolLocator">The tool locator.</param>
        public CoverallsIoRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator toolLocator)
            : base(fileSystem, environment, processRunner, toolLocator)
        {
            _environment = environment;
        }

        /// <summary>
        /// Publish the code coverage report to Coveralls.io using the specified settings.
        /// </summary>
        /// <param name="codeCoverageReportFilePath">The code coverage report file path.</param>
        /// <param name="settings">The settings.</param>
        public void Run(FilePath codeCoverageReportFilePath, CoverallsIoSettings settings)
        {
            ArgumentNullException.ThrowIfNull(codeCoverageReportFilePath);
            ArgumentNullException.ThrowIfNull(settings);

            Run(settings, GetArguments(codeCoverageReportFilePath, settings));
        }

        /// <summary>
        /// Gets the name of the tool.
        /// </summary>
        /// <returns>The name of the tool.</returns>
        protected override string GetToolName()
        {
            return "CoverallsIo";
        }

        /// <summary>
        /// Gets the name of the tool executable.
        /// </summary>
        /// <returns>The tool executable name.</returns>
        protected override IEnumerable<string> GetToolExecutableNames()
        {
            return new[] { "coveralls.net.exe" };
        }

        private static string GetReportType(CoverallsIoReportType reportType) => reportType switch
        {
            CoverallsIoReportType.OpenCover => "--opencover",
            CoverallsIoReportType.Cobertura => "--cobertura",
            _ => throw new NotSupportedException("The provided output is not valid."),
        };

        private ProcessArgumentBuilder GetArguments(FilePath codeCoverageReportFilePath, CoverallsIoSettings settings)
        {
            var builder = new ProcessArgumentBuilder();
            builder.Append(GetReportType(settings.ReportType));
            builder.AppendQuoted(codeCoverageReportFilePath.MakeAbsolute(_environment).FullPath);

            if (settings.Debug)
            {
                builder.Append("--debug");
            }

            if (settings.FullSources)
            {
                builder.Append("--full-sources");
            }

            if (!string.IsNullOrWhiteSpace(settings.RepoToken))
            {
                builder.Append("--repo-token");
                builder.AppendQuotedSecret(settings.RepoToken);
            }

            return builder;
        }
    }
}
