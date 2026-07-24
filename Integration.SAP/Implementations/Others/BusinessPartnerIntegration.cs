using Application.DataTransferObjects.Others;
using Application.DataTransferObjects.Others.SAP;
using Application.UseCases.Repositories.Integration.Others;
using Database.Libraries.Repositories;
using Integration.Sap.Entities;
using Integration.Sap.Helpers;
using Integration.Sap.Repositories;
using Shared.Libraries.Entities;

namespace Integration.SAP.Implementations.Others;

public class BusinessPartnerIntegration(
    ISqlQueryManager qryManager,
    IServiceLayerActions SLActions)
    : IBusinessPartnerIntegration
{
    public async Task<(IEnumerable<BusinessPartnerSAPDTO> Data, int Count)> GetAllAsync(DataGridIntent intent)
    {
        if (intent.Sorts.Count <= 0)
        {
            intent.Sorts.Add(new AppSortDescriptor
            {
                Property = "CardName",
                Direction = SortDirectionEnum.Ascending
            });
        }

        var qryDetails = qryManager.GetSqlScriptWithMetadata("APHI_Others_AllBps", out string qry, out bool found);
        if (!found)
            throw new Exception("Base query for getting all Business Partners not found.");

        string query = DataGridQueryBuilder.BuildQuery(qry, intent);
        string countQuery = DataGridQueryBuilder.BuildCountQuery(qry, intent);

        List<BusinessPartnerSAPDTO> data = await SLActions.RawQueryAsync<BusinessPartnerSAPDTO>(query);
        TotalRows? rowCount = await SLActions.RawQueryOneAsync<TotalRows>(countQuery);

        return (data, rowCount?.Count ?? data.Count);
    }

    public async Task<(IEnumerable<BusinessPartnerSAPDTO> Data, int Count)> GetCustomersAsync(DataGridIntent intent)
    {
        if (intent.Sorts.Count <= 0)
        {
            intent.Sorts.Add(new AppSortDescriptor
            {
                Property = "CardName",
                Direction = SortDirectionEnum.Ascending
            });
        }

        var qryDetails = qryManager.GetSqlScriptWithMetadata("APHI_Others_Customers", out string qry, out bool found);
        if (!found)
            throw new Exception("Base query for getting all Customers not found.");

        string query = DataGridQueryBuilder.BuildQuery(qry, intent);
        string countQuery = DataGridQueryBuilder.BuildCountQuery(qry, intent);

        List<BusinessPartnerSAPDTO> data = await SLActions.RawQueryAsync<BusinessPartnerSAPDTO>(query);
        TotalRows? rowCount = await SLActions.RawQueryOneAsync<TotalRows>(countQuery);

        return (data, rowCount?.Count ?? data.Count);
    }

    public async Task<(IEnumerable<BusinessPartnerSAPDTO> Data, int Count)> GetVendorsAsync(DataGridIntent intent)
    {
        if (intent.Sorts.Count <= 0)
        {
            intent.Sorts.Add(new AppSortDescriptor
            {
                Property = "CardName",
                Direction = SortDirectionEnum.Ascending
            });
        }

        var qryDetails = qryManager.GetSqlScriptWithMetadata("APHI_Others_Vendors", out string qry, out bool found);
        if (!found)
            throw new Exception("Base query for getting all Vendors not found.");

        string query = DataGridQueryBuilder.BuildQuery(qry, intent);
        string countQuery = DataGridQueryBuilder.BuildCountQuery(qry, intent);

        List<BusinessPartnerSAPDTO> data = await SLActions.RawQueryAsync<BusinessPartnerSAPDTO>(query);
        TotalRows? rowCount = await SLActions.RawQueryOneAsync<TotalRows>(countQuery);

        return (data, rowCount?.Count ?? data.Count);
    }

    public async Task<IEnumerable<WarehouseDTO>> GetBusinessPartnerWarehouses(string bpCode)
    {
        DataGridIntent FilterIntent = new DataGridIntent()
        {
            Take = 9999, // feelsbadman,
            Skip = 0
        };

        FilterIntent.Filters.Add(new AppFilterDescriptor()
        {
            Value = bpCode,
            Property = "CardCode",
            ComparisonOperator = ComparisonOperatorEnum.Equals
        });

        var qryDetails = qryManager.GetSqlScriptWithMetadata("APHI_Others_BP_Warehouses", out string qry, out bool found);
        if (!found)
            throw new Exception("Base query for getting all business partner warehouses not found.");

        string query = DataGridQueryBuilder.BuildQuery(qry, FilterIntent);
        string countQuery = DataGridQueryBuilder.BuildCountQuery(qry, FilterIntent);

        List<WarehouseCardCodeDTO> data = await SLActions.RawQueryAsync<WarehouseCardCodeDTO>(query);
        TotalRows? rowCount = await SLActions.RawQueryOneAsync<TotalRows>(countQuery);

        return data.Select(x => new WarehouseDTO() { 
            WhsCode = x.WhsCode, 
            WhsName = x.WhsName 
        });
    }

    private class WarehouseCardCodeDTO
    {
        public string WhsCode {get; set;} = string.Empty;
        public string WhsName { get; set; } = string.Empty;
	    public string CardCode {get; set;} = string.Empty;
    }
}
