#r "Cake.Coveralls.dll"

try
{
    //CoverallsIo("C:/github/gep13/Cake.Coveralls/Examples/OpenCover-Cake.Gem.xml", new CoverallsIoSettings()
    //{
    //     RepoToken = "6HmL41WzJXMmx6sMycnlhtACNLm24iBxp"
    //});

    CoverallsNet("C:/github/gep13/Cake.Coveralls/Examples/OpenCover-Cake.Tfx.xml", CoverallsNetReportType.OpenCover, new CoverallsNetSettings()
    {
         RepoToken = "Y5g249qPgSXhz8JFH5mOtsiSJvXOGWtEX"
    });
}
catch(Exception ex)
{
    Error("{0}", ex);
}
