#!/usr/bin/env dotnet
#:sdk Cake.Sdk@6.1.1
#:project ../../Source/Cake.Coveralls/Cake.Coveralls.csproj
#:property Nullable=enable

// Nullable=enable propagates `<Nullable>enable</Nullable>` to the
// SDK's synthetic csproj so the addin's `string?` / `FilePath?` /
// `DirectoryPath?` annotations on CoverallsIoSettings /
// CoverallsNetSettings load in the right context (silences CS8632).
//
// NoWarn=CS8603 silences "Possible null reference return" warnings
// emitted from Cake.Sdk's generated `CakeMethodAliases.g.cs` — the
// source generator doesn't currently propagate the addin's `?`
// annotations into the synthesized wrappers, so it reports the
// nullable returns as suspicious. Not actionable from our code;
// upstream issue against Cake.Sdk.
#:property NoWarn=CS8603

// Cake SDK consumer demo for Cake.Coveralls. Runs as a file-based
// .NET program (introduced in .NET 10) using the Cake.Sdk
// directives. The #:project directive above lets the SDK build the
// addin from source rather than referencing a published nupkg.
//
// To run locally:
//   cd demo/sdk
//   dotnet cake.cs
//
// Runs the same three checks the script and frosting demos run.

using Cake.Coveralls;

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

// ----- Helpers (must come AFTER top-level statements per CS8803) -----

static void AssertThat(bool condition, string message)
{
    if (!condition)
    {
        throw new Exception("Assertion failed: " + message);
    }
}
