using Application.DataTransferObjects.System.Settings;
using Application.UseCases.Repositories.Bases;
using Domain.Entities.Entities.System;
using MediatR;

namespace Application.UseCases.Commands.System.Settings;

public record UpdateSettingsListCmd(IEnumerable<SettingsDTO> Settings) : ITransactionalRequest<bool>;

public class UpdateSettingsListCmdHandler(
    IAppReadRepository appReadRepo,
    IAppCommandRepository appCommandRepo)
    : IRequestHandler<UpdateSettingsListCmd, bool>
{
    public async Task<bool> Handle(UpdateSettingsListCmd request, CancellationToken cancellationToken)
    {
        List<SettingsDEM> dems = [];

        foreach (SettingsDTO dto in request.Settings)
        {
            SettingsDEM? dem = await appReadRepo.FirstOrDefaultAsync<SettingsDEM>(x => x.Id == dto.Id, track: true);

            if (dem is null)
                throw new Exception($"Setting with ID {dto.Id} not found.");

            dem.Update(dto.Name, dto.Description, dto.Type.ToString(), dto.Value);
            dems.Add(dem);
        }

        appCommandRepo.UpdateMany(dems);

        return true;
    }
}
