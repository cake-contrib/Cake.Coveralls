using Build.Tasks;
using Cake.Frosting;

namespace Build
{
    [TaskName("Default")]
    [IsDependentOn(typeof(SettingsCoverallsIoTask))]
    [IsDependentOn(typeof(SettingsCoverallsNetTask))]
    [IsDependentOn(typeof(ReportTypeEnumsTask))]
    public sealed class DefaultTask : FrostingTask
    {
    }
}
