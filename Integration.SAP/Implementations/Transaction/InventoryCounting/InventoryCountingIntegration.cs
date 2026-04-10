using Application.DataTransferObjects.Transactions.InventoryCounting;
using Application.UseCases.Repositories.Integration.Transaction.InventoryCounting;
using Database.Libraries.Repositories;
using Integration.Sap.Repositories;
using Integration.SAP.Entities.Transactional.InventoryCounting;
using System.Text.Json;

namespace Integration.SAP.Implementations.Transaction.InventoryCounting;

public class InventoryCountingIntegration(
    ISqlQueryManager qryManager,
    IServiceLayerActions SLActions)
    : IInventoryCountingIntegration
{
    public async Task<bool> PostInventoryCountings(InventoryCountingDocumentDTO data)
    {
        // Re-fetch correct UoM codes directly from SAP at post time.
        // Uses a dedicated lookup query (no OnHand filter) to ensure all document lines
        // are covered even if stock has reached zero since the document was created.
        qryManager.GetSqlScriptWithMetadata("APHI_InventoryCounting_UomLookup", out string qry, out bool found);
        if (!found) throw new Exception("UoM lookup query for inventory counting was not found.");

        IEnumerable<ItemUomLookup> uomItems =
            await SLActions.RawQueryAsync<ItemUomLookup>(qry.Replace("{WhsCode}", data.Warehouse.WhsCode));

        Dictionary<string, string> uomLookup = uomItems
            .GroupBy(i => i.ItemCode, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(g => g.Key, g => g.First().UoMCode ?? string.Empty, StringComparer.OrdinalIgnoreCase);

        List<InventoryCountingsLinesPayload> payloadLines = [];

        foreach (InventoryCountingDocumentLineDTO line in data.DocumentLines)
        {
            string uomCode = uomLookup.TryGetValue(line.ItemCode, out string? freshUom) && !string.IsNullOrEmpty(freshUom)
                ? freshUom
                : line.UoMCode;
            payloadLines.Add(new(line.ItemCode, data.Warehouse.WhsCode, uomCode, line.ActualQuantity));
        }

        InventoryCountingsPayload payload = new(data.CountingDate,
                                                string.IsNullOrEmpty(data.PrepBy) ? "WMS User" : data.PrepBy,
                                                data.Remarks ?? "WMS User",
                                                payloadLines);

        string jsonString = JsonSerializer.Serialize(payload, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        await SLActions.PostAsync<object, InventoryCountingsPayload>("InventoryCountings", payload);


        return true;
    }
}
