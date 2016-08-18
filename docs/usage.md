# Build Script

Within your build script, you will need to add the following (normally near the top of the file):

```csharp
#addin Cake.Coveralls
```

and either one of the following:

```csharp
#tools coveralls.net
```

```csharp
#tool coveralls.io
```

And then you will be able to utilise one of the available Aliases.  An example of each can be seen below:

```csharp
Task("Upload-Coverage-Report")
    .Does(() =>
{
    CoverallsIo("coverage.xml");
});
```

```csharp
Task("Upload-Coverage-Report")
    .Does(() =>
{
    CoverallsIo("coverage.xml", new CoverallsIoSettings()
    {
        RepoToken = "abcdef"
    });
});
```

```csharp
Task("Upload-Coverage-Report")
    .Does(() =>
{
    CoverallsIo("coverage.xml", CoverallsNetReportType.OpenCover);
});
```

```csharp
Task("Upload-Coverage-Report")
    .Does(() =>
{
    CoverallsNet("coverage.xml", CoverallsNetReportType.OpenCover, new CoverallsNetSettings()
    {
        RepoToken = "abcdef"
    });
});
```