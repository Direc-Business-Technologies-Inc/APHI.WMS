using Shared.Libraries.Kernel;
using Web.BlazorServer.ViewModels.Commons;

namespace Web.BlazorServer.ViewModels.Administration.Settings;

public class SettingsVM : EntityVM
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public AppTypes Type { get; set; }
    public string Value { get; set; } = string.Empty;
}
