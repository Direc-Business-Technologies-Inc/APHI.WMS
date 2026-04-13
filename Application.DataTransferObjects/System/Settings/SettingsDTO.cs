using Application.DataTransferObjects.Commons;

namespace Application.DataTransferObjects.System.Settings;

public class SettingsDTO : EntityDTO
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Type { get; set; }
    public string Value { get; set; }
}
