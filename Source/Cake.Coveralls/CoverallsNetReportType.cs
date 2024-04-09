namespace Cake.Coveralls
{
    /// <summary>
    /// The coverage report type for CoverallNetRunner.
    /// </summary>
    public enum CoverallsNetReportType
    {
        /// <summary>
        /// Reads input as OpenCover data.
        /// </summary>
        OpenCover,

        /// <summary>
        /// Reads input as the CodeCoverage.exe xml format.
        /// </summary>
        DynamicCodeCoverage,

        /// <summary>
        /// Reads input as monocov results folder.
        /// </summary>
        Monocov,
    }
}
