using Shared.Libraries.Kernel;

namespace Application.DataTransferObjects.System.Settings;

public class SettingsDataGridDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public AppTypes Type { get; set; }
    public string Value { get; set; }
}
