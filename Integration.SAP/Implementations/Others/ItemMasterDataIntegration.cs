using Application.DataTransferObjects.Others.SAP;
using Application.DataTransferObjects.Transactions.GoodsReturn.SAP;
using Application.UseCases.Repositories.Integration.Others;
using Database.Libraries.Repositories;
using Integration.Sap.Entities;
using Integration.Sap.Helpers;
using Integration.Sap.Repositories;
using Shared.Entities;

namespace Integration.SAP.Implementations.Others;

public class ItemMasterDataIntegration (
    ISqlQueryManager qryManager,
    IServiceLayerActions SLActions)
    : IItemMasterDataIntegration
{
    public async Task<(IEnumerable<ItemSelectionSAPDTO> Data, int Count)> GetMerchandiseItems(DataGridIntent intent)
    {
        Dictionary<string, string> columnMap = new()
            {
                { "ItemCode", "T0.ItemCode" },
                { "ItemName", "T0.ItemName" },
                { "Quantity", "T0.OnHand" },
                { "UoMCode", "T0.InvntryUom" },
                { "UoMValue", "T4.BaseQty" },
                { "UoMName", "T2.UomName" },
            };

        if (intent.Sorts.Count <= 0)
        {
            intent.Sorts.Add(new AppSortDescriptor
            {
                Property = "ItemCode",
                Direction = SortDirectionEnum.Descending
            });
        }

        var qryDetails = qryManager.GetSqlScriptWithMetadata("APHI_Others_ItemSelection", out string qry, out bool found);
        if (!found)
            throw new Exception("Query for getting all items not found.");

        string query = DataGridQueryBuilder.BuildQuery(qry, intent);
        string countQuery = DataGridQueryBuilder.BuildCountQuery(qry, intent.Filters, columnMap);

        List<ItemSelectionSAPDTO> items = await SLActions.RawQueryAsync<ItemSelectionSAPDTO>(query);
        TotalRows? rowCount = await SLActions.RawQueryOneAsync<TotalRows>(countQuery);

        return (items, rowCount?.Count ?? items.Count);
    }
}
