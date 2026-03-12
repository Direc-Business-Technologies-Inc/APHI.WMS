using Application.DataTransferObjects.Transactions.SalesReturn.SAP;
using Shared.Entities;

namespace Application.UseCases.Repositories.Integration.Transaction.SalesReturn;

public interface ISalesReturnIntegration
{
    Task<(IEnumerable<SalesReturnDataGridSAPDTO> Data, int Count)> GetSalesReturnDataAsync(DataGridIntent intent);
    Task<SalesReturnHeaderSAPDTO?> GetSalesReturnHeaderAsync(int docEntry);
    Task<IEnumerable<SalesReturnLinesSAPDTO>> GetSalesReturnLinesAsync(int docEntry);
    Task<(IEnumerable<SalesReturnRequestDataGridSAPDTO> Data, int Count)> GetSalesReturnRequestDataAsync(DataGridIntent intent);
    Task<SalesReturnRequestHeaderSAPDTO?> GetSalesReturnRequestHeaderAsync(int docEntry);
    Task<IEnumerable<SalesReturnRequestLinesSAPDTO>> GetSalesReturnRequestLinesAsync(int docEntry);
    Task<IEnumerable<ReturnTypeSAPDTO>> GetReturnTypesAsync();
}
