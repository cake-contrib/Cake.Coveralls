using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.Coveralls
{
    /// <summary>
    /// Contains settings used by <see cref="CoverallsNetRunner"/>.
    /// </summary>
    public sealed class CoverallsNetSettings : ToolSettings
    {
        /// <summary>
        /// Gets or sets the Output File path.
        /// </summary>
        public FilePath OutputFilePath { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to attempt to strip the current working directory from the beginning of the source file path.
        /// </summary>
        public bool UseRelativePaths { get; set; }

        /// <summary>
        /// Gets or sets the Base File path, which when UseRelativePaths is set, this path is used instead of the current working directory.
        /// </summary>
        public DirectoryPath BaseFilePath { get; set; }

        /// <summary>
        /// Gets or sets the Repo Token to use when publishing to Coveralls.io
        /// </summary>
        public string RepoToken { get; set; }

        /// <summary>
        /// Gets or sets the Repo Token Variable which is the Environment Variable name where the coverall.io token is available.
        /// </summary>
        public string RepoTokenVariable { get; set; }

        /// <summary>
        /// Gets or sets the git commit hash for coverage report.
        /// </summary>
        public string CommitId { get; set; }

        /// <summary>
        /// Gets or sets the git commit branch for coverage report.
        /// </summary>
        public string CommitBranch { get; set; }

        /// <summary>
        /// Gets or sets the git commit author for coverage report.
        /// </summary>
        public string CommitAuthor { get; set; }

        /// <summary>
        /// Gets or sets the git commit email for coverage report.
        /// </summary>
        public string CommitEmail { get; set; }

        /// <summary>
        /// Gets or sets the git commit message for coverage report.
        /// </summary>
        public string CommitMessage { get; set; }

        /// <summary>
        /// Gets or sets the Job Id to provide to coveralls.io.
        /// </summary>
        /// <remarks>Default is 0.</remarks>
        public string JobId { get; set; }

        /// <summary>
        /// Gets or sets the service name for the coverage report.
        /// </summary>
        /// <remarks>Default is coveralls.net</remarks>
        public string ServiceName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to exit successfully if an upload error is encountered.
        /// </summary>
        public bool TreatUploadErrorsAsWarnings { get; set; }
    }
}