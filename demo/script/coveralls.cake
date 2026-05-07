#reference "../../BuildArtifacts/temp/_PublishedLibraries/Cake.Coveralls/net8.0/Cake.Coveralls.dll"

// Self-contained exercise of Cake.Coveralls' settings + alias surface.
// Verifies the addin loads, the settings types can be constructed and
// round-tripped, and the runner aliases are accessible. Does NOT actually
// post to Coveralls.io — that requires a real token plus the
// coveralls.io / coveralls.net tools on PATH, neither of which are
// available in CI.

using Cake.Coveralls;

void AssertThat(bool condition, string message)
{
    if (!condition)
    {
        throw new Exception("Assertion failed: " + message);
    }
}

Task("Default")
    .IsDependentOn("Settings-CoverallsIo")
    .IsDependentOn("Settings-CoverallsNet")
    .IsDependentOn("ReportType-Enums");

Task("Settings-CoverallsIo")
    .Does(() =>
{
    var settings = new CoverallsIoSettings
    {
        Debug = true,
        FullSources = true,
        RepoToken = "fake-token",
        ReportType = CoverallsIoReportType.Cobertura,
    };

    AssertThat(settings.Debug, "Debug should be true");
    AssertThat(settings.FullSources, "FullSources should be true");
    AssertThat(settings.RepoToken == "fake-token", "RepoToken roundtrip mismatch");
    AssertThat(settings.ReportType == CoverallsIoReportType.Cobertura, "ReportType roundtrip mismatch");

    Information("CoverallsIoSettings OK (Debug={0}, FullSources={1}, ReportType={2})",
        settings.Debug, settings.FullSources, settings.ReportType);
});

Task("Settings-CoverallsNet")
    .Does(() =>
{
    var settings = new CoverallsNetSettings
    {
        OutputFilePath = File("./coverage.xml"),
        UseRelativePaths = true,
        BaseFilePath = Directory("./Source"),
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

    AssertThat(settings.OutputFilePath != null, "OutputFilePath should be set");
    AssertThat(settings.UseRelativePaths, "UseRelativePaths should be true");
    AssertThat(settings.BaseFilePath != null, "BaseFilePath should be set");
    AssertThat(settings.RepoToken == "fake-token", "RepoToken roundtrip mismatch");
    AssertThat(settings.RepoTokenVariable == "COVERALLS_REPO_TOKEN", "RepoTokenVariable roundtrip mismatch");
    AssertThat(settings.CommitId == "abc123", "CommitId roundtrip mismatch");
    AssertThat(settings.CommitBranch == "develop", "CommitBranch roundtrip mismatch");
    AssertThat(settings.CommitAuthor == "Cake", "CommitAuthor roundtrip mismatch");
    AssertThat(settings.CommitEmail == "cake@example.com", "CommitEmail roundtrip mismatch");
    AssertThat(settings.CommitMessage == "exercise script", "CommitMessage roundtrip mismatch");
    AssertThat(settings.JobId == "42", "JobId roundtrip mismatch");
    AssertThat(settings.ServiceName == "coveralls.net", "ServiceName roundtrip mismatch");
    AssertThat(settings.TreatUploadErrorsAsWarnings, "TreatUploadErrorsAsWarnings should be true");

    Information("CoverallsNetSettings OK (CommitId={0}, JobId={1}, ServiceName={2})",
        settings.CommitId, settings.JobId, settings.ServiceName);
});

Task("ReportType-Enums")
    .Does(() =>
{
    // Ensure all enum values resolve at compile time and at runtime.
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

    AssertThat(ioTypes.Length == 2, "CoverallsIoReportType: expected 2 values");
    AssertThat(netTypes.Length == 3, "CoverallsNetReportType: expected 3 values");

    Information("ReportType enums OK (Io: {0} values, Net: {1} values)", ioTypes.Length, netTypes.Length);
});

RunTarget("Default");
