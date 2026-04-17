using Application.DataTransferObjects.Transactions.InventoryTransfer.SAP;
using Application.UseCases.Repositories.Integration.Others;
using Database.Libraries.Repositories;
using Integration.Sap.Repositories;

namespace Integration.SAP.Implementations.Transaction.InventoryTransfer;

public class TransferTypeIntegration(
    ISqlQueryManager qryManager,
    IServiceLayerActions SLActions) : ITransferTypeIntegration
{
    public async Task<IEnumerable<TransferTypeSAPDTO>> GetTransferTypesAsync()
    {
        var qryDetails = qryManager.GetSqlScriptWithMetadata("APHI_InventoryTransfer_TransferTypes", out string qry, out bool found);
        if (!found)
            throw new Exception("Query for Transaction Types not found.");

        List<TransferTypeSAPDTO> data = await SLActions.RawQueryAsync<TransferTypeSAPDTO>(qry);

        return data;
    }
}
