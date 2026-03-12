using Application.DataTransferObjects.Transactions.Delivery;
using Application.UseCases.Queries.Transaction.Delivery;
using MediatR;
using Shared.Entities;
using Web.BlazorServer.Handlers.Repositories.Transaction.Delivery;
using Web.BlazorServer.ViewModels.Transaction.Delivery;

namespace Web.BlazorServer.Handlers.Implementations.Transaction.Delivery;

public class DeliveryHandler(
    ISender Sender) 
    : IDeliveryHandler
{
    public async Task<DeliveryVM?> GetDeliveryAsync(int docEntry)
    {
        throw new NotImplementedException();
    }

    public Task<(IEnumerable<DeliveryDataGridVM> Data, int Count)> GetDeliveryDataGridAsync(DataGridIntent intent)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<DeliveryMeansVM>> GetDeliveryMeansAsync()
    {
        throw new NotImplementedException();
    }

    public Task<SalesOrderVM?> GetSalesOrderAsync(int docEntry)
    {
        throw new NotImplementedException();
    }

    public Task<(IEnumerable<SalesOrderDataGridVM> Data, int Count)> GetSalesOrderDataGridAsync(DataGridIntent intent)
    {
        throw new NotImplementedException();
    }

    public Task<bool> PostDeliveryAsync(SalesOrderVM data)
    {
        throw new NotImplementedException();
    }
}
