using Application.DataTransferObjects.Others.SAP;
using Application.UseCases.Repositories.Integration.Others;
using Database.Libraries.Repositories;
using Integration.Sap.Repositories;

namespace Integration.SAP.Implementations.Others;

public class DashboardItemsIntegration(
    ISqlQueryManager qryManager,
    IServiceLayerActions SLActions)
    : IDashboardItemsIntegration
{
    public async Task<DashboardItemsSAPDTO> GetDashboardItemsAsync()
    {
        var qryDetails = qryManager.GetSqlScriptWithMetadata("APHI_Others_Dashboard", out string qry, out bool found);
        if (!found)
            throw new Exception("Query for Dashboard Items not found.");
        var data = await SLActions.RawQueryAsync<DashboardItemsSAPDTO>(qry);

        return data.First();
    }
}
