using Application.DataTransferObjects.Others.SAP;
using Application.UseCases.Repositories.Integration.Others;
using Database.Libraries.Repositories;
using Integration.Sap.Entities;
using Integration.Sap.Helpers;
using Integration.Sap.Repositories;
using Shared.Libraries.Entities;

namespace Integration.SAP.Implementations.Others;

public class ItemGroupIntegration(
    ISqlQueryManager qryManager,
    IServiceLayerActions SLActions)
    : IItemGroupsIntegration
{
    public async Task<(IEnumerable<ItemGroupSAPDTO> Data, int Count)> GetItemGroupsAsync(DataGridIntent intent)
    {
        Dictionary<string, string> columnMap = new()
            {
                { "Code", "Code" },
                { "Name", "Name" },
            };

        if (intent.Sorts.Count <= 0)
        {
            intent.Sorts.Add(new AppSortDescriptor
            {
                Property = "Name",
                Direction = SortDirectionEnum.Ascending
            });
        }

        var qryDetails = qryManager.GetSqlScriptWithMetadata("APHI_Others_ItemGroups", out string qry, out bool found);
        if (!found)
            throw new Exception("Base query for getting item groups not found.");

        string query = DataGridQueryBuilder.BuildQuery(qry, intent);
        string countQuery = DataGridQueryBuilder.BuildCountQuery(qry, intent);

        List<ItemGroupSAPDTO> data = await SLActions.RawQueryAsync<ItemGroupSAPDTO>(query);
        TotalRows? rowCount = await SLActions.RawQueryOneAsync<TotalRows>(countQuery);

        return (data, rowCount?.Count ?? data.Count);
    }
}
