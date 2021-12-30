using Cake.Core.Tooling;

namespace Cake.Coveralls
{
    /// <summary>
    /// Contains settings used by <see cref="CoverallsIoRunner"/>.
    /// </summary>
    public sealed class CoverallsIoSettings : ToolSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether to turn on debugging.
        /// </summary>
        public bool Debug { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to send full sources instead of the digest.
        /// </summary>
        public bool FullSources { get; set; }

        /// <summary>
        /// Gets or sets the Repo Token to use when publishing to <see href="https://coveralls.io/">Coveralls.io</see>.
        /// </summary>
        public string RepoToken { get; set; }

        public CoverageParseType ParseType { get; set; } = CoverageParseType.opencover;
    }
}
