using Domain.Entities.Enums.Transaction.SalesReturn;
using Shared.Libraries.Entities;
using Web.BlazorServer.ViewModels.Transaction.SalesReturn;

namespace Web.BlazorServer.Handlers.Repositories.Transaction.SalesReturn;

public interface ISalesReturnHandler
{
    Task<(IEnumerable<SalesReturnDataGridVM> Data, int Count)> GetSalesReturnDataGridAsync(DataGridIntent intent);
    Task<SalesReturnVM?> GetSalesReturnAsync(int docEntry);
    Task<(IEnumerable<SalesReturnRequestDataGridVM> Data, int Count)> GetSalesReturnRequestDataGridAsync(DataGridIntent intent);
    Task<SalesReturnRequestVM?> GetSalesReturnRequestAsync(int docEntry);
    Task<IEnumerable<ReturnTypeVM>> GetReturnTypesAsync();
    Task<bool> PostSalesReturnAsync(SalesReturnVM data, SalesReturnPostingSource source);
    Task<bool> PostSalesReturnFromRequestAsync(SalesReturnRequestVM data);
    Task<(IEnumerable<SalesReturnDataGridVM> Data, int Count)> GetSalesReturnDraftDataGridAsync(DataGridIntent intent);
    Task<SalesReturnVM?> GetSalesReturnDraftAsync(int docEntry);
}
