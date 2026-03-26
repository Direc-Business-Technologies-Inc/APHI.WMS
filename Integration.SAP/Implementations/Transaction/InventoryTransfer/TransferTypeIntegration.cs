using Application.DataTransferObjects.Others.SAP;
using Application.UseCases.Repositories.Integration.Others;
using Database.Libraries.Repositories;
using Integration.Sap.Entities;
using Integration.Sap.Helpers;
using Integration.Sap.Repositories;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.SAP.Implementations.Transaction.InventoryTransfer;

public class TransferTypeIntegration(
    ISqlQueryManager qryManager,
    IServiceLayerActions SLActions) : ITransferTypeIntegration
{
    public async Task<IEnumerable<TransferTypeSAPDTO>> GetTransferTypesAsync()
    {
        var qryDetails = qryManager.GetSqlScriptWithMetadata("APHI_Others_TransferTypes", out string qry, out bool found);
        if (!found)
            throw new Exception("Query for Transaction Types not found.");

        List<TransferTypeSAPDTO> data = await SLActions.RawQueryAsync<TransferTypeSAPDTO>(qry);

        return data;
    }
}
