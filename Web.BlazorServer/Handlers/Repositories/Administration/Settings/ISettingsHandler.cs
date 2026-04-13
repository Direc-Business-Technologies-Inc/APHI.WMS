using Web.BlazorServer.ViewModels.Administration.Settings;

namespace Web.BlazorServer.Handlers.Repositories.Administration.Settings;

public interface ISettingsHandler
{
    Task<IEnumerable<SettingsVM>> GetAllSettingsAsync();
    Task<SettingsVM?> GetSettingByIdAsync(Guid id);
    Task<SettingsVM?> GetSettingByNameAsync(string name);
    Task<bool> UpdateSettingsListAsync(IEnumerable<SettingsVM> settings);
}
