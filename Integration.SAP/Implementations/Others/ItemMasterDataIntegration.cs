using Application.DataTransferObjects.Others.SAP;
using Application.UseCases.Repositories.Integration.Others;
using Database.Libraries.Repositories;
using Integration.Sap.Entities;
using Integration.Sap.Helpers;
using Integration.Sap.Repositories;
using Shared.Libraries.Entities;

namespace Integration.SAP.Implementations.Others;

public class ItemMasterDataIntegration(
    ISqlQueryManager qryManager,
    IServiceLayerActions SLActions)
    : IItemMasterDataIntegration
{
    public async Task<(IEnumerable<ItemSelectionSAPDTO> Data, int Count)> GetItemWarehouseLevel(DataGridIntent intent, string whsCode, List<string> itemCodes)
    {
        if (intent.Sorts.Count <= 0)
        {
            intent.Sorts.Add(new AppSortDescriptor
            {
                Property = "ItemCode",
                Direction = SortDirectionEnum.Descending
            });
        }

        intent.Filters.Add(new AppFilterDescriptor()
        {
            LogicalOperator = LogicalOperatorEnum.AND,
            Property = "WhsCode",
            Value = whsCode,
            FilterValueType = FilterValueTypeEnum.String,
            ComparisonOperator = ComparisonOperatorEnum.Contains,
        });

        intent.Filters.Add(new AppFilterDescriptor()
        {
            LogicalOperator = LogicalOperatorEnum.AND,
            Property = "ItemCode",
            Value = itemCodes,
            FilterValueType = FilterValueTypeEnum.String,
            ComparisonOperator = ComparisonOperatorEnum.In,
        });

        var qryDetails = qryManager.GetSqlScriptWithMetadata("APHI_Others_WhsItemsSelection", out string qry, out bool found);
        if (!found)
            throw new Exception("Query for getting all items not found.");

        string query = DataGridQueryBuilder.BuildQuery(qry, intent);
        string countQuery = DataGridQueryBuilder.BuildCountQuery(qry, intent);

        List<ItemSelectionSAPDTO> items = await SLActions.RawQueryAsync<ItemSelectionSAPDTO>(query);
        TotalRows? rowCount = await SLActions.RawQueryOneAsync<TotalRows>(countQuery);

        return (items, rowCount?.Count ?? items.Count);
    }

    public async Task<(IEnumerable<ItemSelectionSAPDTO> Data, int Count)> GetMerchandiseItems(DataGridIntent intent)
    {
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
        string countQuery = DataGridQueryBuilder.BuildCountQuery(qry, intent);

        List<ItemSelectionSAPDTO> items = await SLActions.RawQueryAsync<ItemSelectionSAPDTO>(query);
        TotalRows? rowCount = await SLActions.RawQueryOneAsync<TotalRows>(countQuery);

        return (items, rowCount?.Count ?? items.Count);
    }

    public async Task<IEnumerable<InventoryCountingItemSAPDTO>> GetWarehouseItemsForCounting(string whsCode)
    {
        var qryDetails = qryManager.GetSqlScriptWithMetadata("APHI_Others_WhsItemsForCounting", out string qry, out bool found);
        if (!found)
            throw new Exception("Query for getting warehouse items for counting was not found.");

        string query = qry.Replace("{WhsCode}", whsCode);

        return await SLActions.RawQueryAsync<InventoryCountingItemSAPDTO>(query);
    }

    public async Task<(IEnumerable<ItemSelectionSAPDTO> Data, int Count)> GetWarehouseItems(DataGridIntent intent, string whsCode)
    {
        if (intent.Sorts.Count <= 0)
        {
            intent.Sorts.Add(new AppSortDescriptor
            {
                Property = "ItemCode",
                Direction = SortDirectionEnum.Descending
            });
        }

        intent.Filters.Add(new AppFilterDescriptor()
        {
            LogicalOperator = LogicalOperatorEnum.AND,
            Property = "WhsCode",
            Value = whsCode,
            FilterValueType = FilterValueTypeEnum.String,
            ComparisonOperator = ComparisonOperatorEnum.Contains,
        });

        var qryDetails = qryManager.GetSqlScriptWithMetadata("APHI_Others_WhsItemsSelection", out string qry, out bool found);
        if (!found)
            throw new Exception("Query for getting all items not found.");

        string query = DataGridQueryBuilder.BuildQuery(qry, intent);
        string countQuery = DataGridQueryBuilder.BuildCountQuery(qry, intent);

        List<ItemSelectionSAPDTO> items = await SLActions.RawQueryAsync<ItemSelectionSAPDTO>(query);
        TotalRows? rowCount = await SLActions.RawQueryOneAsync<TotalRows>(countQuery);

        return (items, rowCount?.Count ?? items.Count);
    }
}
