using Application.DataTransferObjects.System.Settings;
using Application.UseCases.Commands.System.Settings;
using Application.UseCases.Queries.System.Settings;
using Mapster;
using MediatR;
using Web.BlazorServer.Handlers.Repositories.Administration.Settings;
using Web.BlazorServer.ViewModels.Administration.Settings;

namespace Web.BlazorServer.Handlers.Implementations.Administration.Settings;

public class SettingsHandler(ISender Sender) : ISettingsHandler
{
    public async Task<IEnumerable<SettingsVM>> GetAllSettingsAsync()
    {
        GetAllSettingsQry qry = new();
        var dtos = await Sender.Send(qry);

        return dtos.Adapt<IEnumerable<SettingsVM>>();
    }

    public async Task<SettingsVM?> GetSettingByIdAsync(Guid id)
    {
        GetSettingByIdQry qry = new(id);
        var dto = await Sender.Send(qry);

        return dto?.Adapt<SettingsVM>();
    }

    public async Task<SettingsVM?> GetSettingByNameAsync(string name)
    {
        GetSettingByNameQry qry = new(name);
        var dto = await Sender.Send(qry);

        return dto?.Adapt<SettingsVM>();
    }

    public async Task<bool> UpdateSettingsListAsync(IEnumerable<SettingsVM> settings)
    {
        var dtos = settings.Adapt<IEnumerable<SettingsDTO>>();

        UpdateSettingsListCmd cmd = new(dtos);
        return await Sender.Send(cmd);
    }
}
