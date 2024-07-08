namespace Cake.Coveralls
{
    /// <summary>
    /// The coverage report type for <see cref="CoverallsIoRunner"/>.
    /// </summary>
    public enum CoverallsIoReportType
    {
        /// <summary>
        /// Reads input as OpenCover data.
        /// </summary>
        OpenCover,

        /// <summary>
        /// Reads input as Cobertura data.
        /// </summary>
        Cobertura,
    }
}
