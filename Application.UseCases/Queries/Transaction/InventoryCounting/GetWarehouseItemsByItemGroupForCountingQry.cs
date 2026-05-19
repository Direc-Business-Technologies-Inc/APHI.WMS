using Application.DataTransferObjects.Others.SAP;
using Application.UseCases.Repositories.Integration.Others;
using MediatR;

namespace Application.UseCases.Queries.Transaction.InventoryCounting;

public record GetWarehouseItemsByItemGroupForCountingQry(string WhsCode, string ItemGroupCode)
    : IRequest<IEnumerable<InventoryCountingItemSAPDTO>>;

public class GetWarehouseItemsByItemGroupForCountingQryHandler(IItemMasterDataIntegration Items)
    : IRequestHandler<GetWarehouseItemsByItemGroupForCountingQry, IEnumerable<InventoryCountingItemSAPDTO>>
{
    public async Task<IEnumerable<InventoryCountingItemSAPDTO>> Handle(
        GetWarehouseItemsByItemGroupForCountingQry request,
        CancellationToken cancellationToken)
        => await Items.GetWarehouseItemsForCounting(request.WhsCode, request.ItemGroupCode);
}
