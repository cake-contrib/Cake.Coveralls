#r "Cake.Coveralls.dll"

try
{
    CoverallsIo("C:/github/gep13/Cake.Coveralls/Examples/OpenCover.xml", new CoverallsIoSettings()
    {
         RepoToken = "6HmL41WzJXMmx6sMycnlhtACNLm24iBxp"
    });
}
catch(Exception ex)
{
    Error("{0}", ex);
}