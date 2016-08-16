using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;

namespace Cake.Coveralls
{
    /// <summary>
    /// Contains aliases related to Coveralls.io
    /// </summary>
    [CakeAliasCategory("Coveralls")]
    public static class CoverallsAliases
    {
        /// <summary>
        /// Uploads the Code Coverage Report to Coveralls.io using the coveralls.io tool.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="codeCoverageReportFilePath">The path to the code coverage file.</param>
        /// <example>
        /// <code>
        /// CoverallsIo("coverage.xml");
        /// </code>
        /// </example>
        [CakeMethodAlias]
        public static void CoverallsIo(this ICakeContext context, FilePath codeCoverageReportFilePath)
        {
            CoverallsIo(context, codeCoverageReportFilePath, new CoverallsIoSettings());
        }

        /// <summary>
        /// Uploads the Code Coverage Report to Coveralls.io using the coveralls.io tool with the specified settings
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="codeCoverageReportFilePath">The path to the code coverage file.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        /// CoverallsIo("coverage.xml", new CoverallsIoSettings()
        /// {
        ///     RepoToken = "abcdef"
        /// });
        /// </code>
        /// </example>
        [CakeMethodAlias]
        public static void CoverallsIo(this ICakeContext context, FilePath codeCoverageReportFilePath, CoverallsIoSettings settings)
        {
            var runner = new CoverallsIoRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            runner.Run(codeCoverageReportFilePath, settings);
        }

        /// <summary>
        /// Uploads the Code Coverage Report to Coveralls.io using the coveralls.net tool.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="codeCoverageReportFilePath">The path to the code coverage file.</param>
        /// <param name="reportType">The type of the Code Coverage Report.</param>
        /// <example>
        /// <code>
        /// CoverallsIo("coverage.xml", CoverallsNetReportType.OpenCover);
        /// </code>
        /// </example>
        [CakeMethodAlias]
        public static void CoverallsNet(this ICakeContext context, FilePath codeCoverageReportFilePath, CoverallsNetReportType reportType)
        {
            CoverallsNet(context, codeCoverageReportFilePath, reportType, new CoverallsNetSettings());
        }

        /// <summary>
        /// Uploads the Code Coverage Report to Coveralls.io using the coveralls.net tool with the specified settings
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="codeCoverageReportFilePath">The path to the code coverage file.</param>
        /// <param name="reportType">The type of the Code Coverage Report.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        /// CoverallsNet("coverage.xml", CoverallsNetReportType.OpenCover, new CoverallsNetSettings()
        /// {
        ///     RepoToken = "abcdef"
        /// });
        /// </code>
        /// </example>
        [CakeMethodAlias]
        public static void CoverallsNet(this ICakeContext context, FilePath codeCoverageReportFilePath, CoverallsNetReportType reportType, CoverallsNetSettings settings)
        {
            var runner = new CoverallsNetRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            runner.Run(codeCoverageReportFilePath, reportType, settings);
        }
    }
}