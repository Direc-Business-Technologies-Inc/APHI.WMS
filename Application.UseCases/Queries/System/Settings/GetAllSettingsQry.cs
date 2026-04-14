using Application.DataTransferObjects.System.Settings;
using Application.UseCases.Repositories.Bases;
using Domain.Entities.Entities.System;
using Mapster;
using MediatR;

namespace Application.UseCases.Queries.System.Settings;

public record GetAllSettingsQry() : IRequest<IEnumerable<SettingsDTO>>;

public class GetAllSettingsQryHandler(
    IAppReadRepository appReadRepo)
    : IRequestHandler<GetAllSettingsQry, IEnumerable<SettingsDTO>>
{
    public async Task<IEnumerable<SettingsDTO>> Handle(GetAllSettingsQry request, CancellationToken cancellationToken)
    {
        List<SettingsDEM> dems = await appReadRepo.GetAllAsync<SettingsDEM>();
        return dems.Adapt<IEnumerable<SettingsDTO>>();
    }
}
