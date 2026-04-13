using Application.DataTransferObjects.System.Settings;
using Application.UseCases.Repositories.Bases;
using Domain.Entities.Entities.System;
using Mapster;
using MediatR;

namespace Application.UseCases.Queries.System.Settings;

public record GetSettingByIdQry(Guid Id) : IRequest<SettingsDTO?>;

public class GetSettingByIdQryHandler(
    IAppReadRepository appReadRepo)
    : IRequestHandler<GetSettingByIdQry, SettingsDTO?>
{
    public async Task<SettingsDTO?> Handle(GetSettingByIdQry request, CancellationToken cancellationToken)
    {
        SettingsDEM? dem = await appReadRepo.FirstOrDefaultAsync<SettingsDEM>(x => x.Id == request.Id);
        return dem?.Adapt<SettingsDTO>();
    }
}
