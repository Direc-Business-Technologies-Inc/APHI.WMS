using Shared.Libraries.Entities;
using Web.BlazorServer.ViewModels.Transaction.Delivery;

namespace Web.BlazorServer.Handlers.Repositories.Transaction.Delivery;

public interface IDeliveryHandler
{
    Task<(IEnumerable<SalesOrderDataGridVM> Data, int Count)> GetSalesOrderDataGridAsync(DataGridIntent intent);
    Task<SalesOrderVM?> GetSalesOrderAsync(int docEntry);
    Task<(IEnumerable<DeliveryDataGridVM> Data, int Count)> GetDeliveryDataGridAsync(DataGridIntent intent);
    Task<(IEnumerable<DeliveryDataGridVM> Data, int Count)> GetDeliveryByCustomerDataGridAsync(DataGridIntent intent, string cardCode);
    Task<DeliveryVM?> GetDeliveryAsync(int docEntry);
    Task<List<DeliveryVM?>> GetDeliveriesAsync(params int[] docEntries);
    Task<bool> PostDeliveryAsync(DeliveryVM data);
    Task<IEnumerable<DeliveryMeansVM>> GetDeliveryMeansAsync();
}
