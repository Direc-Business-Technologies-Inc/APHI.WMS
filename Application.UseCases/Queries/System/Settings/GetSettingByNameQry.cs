using Application.DataTransferObjects.System.Settings;
using Application.UseCases.Repositories.Bases;
using Domain.Entities.Entities.System;
using Mapster;
using MediatR;

namespace Application.UseCases.Queries.System.Settings;

public record GetSettingByNameQry(string Name) : IRequest<SettingsDTO?>;

public class GetSettingByNameQryHandler(
    IAppReadRepository appReadRepo)
    : IRequestHandler<GetSettingByNameQry, SettingsDTO?>
{
    public async Task<SettingsDTO?> Handle(GetSettingByNameQry request, CancellationToken cancellationToken)
    {
        SettingsDEM? dem = await appReadRepo.FirstOrDefaultAsync<SettingsDEM>(x => x.Name.ToLower() == request.Name.ToLower());
        return dem?.Adapt<SettingsDTO>();
    }
}
