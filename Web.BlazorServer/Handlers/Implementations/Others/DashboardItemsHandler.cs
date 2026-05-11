using Application.DataTransferObjects.Others;
using Application.UseCases.Queries.Others;
using Mapster;
using MediatR;
using Shared.Libraries.Entities;
using Web.BlazorServer.Handlers.Repositories.Others;
using Web.BlazorServer.ViewModels.Others;

namespace Web.BlazorServer.Handlers.Implementations.Others;

public class DashboardItemsHandler(
    ISender Sender)
    : IDashboardItemsHandler
{
    public async Task<DashboardItemsVM> GetDashboardItemsAsync()
    {
        GetDashboardItemsQry qry = new();
        var data = await Sender.Send(qry);

        return data.Adapt<DashboardItemsVM>();

    }
}
