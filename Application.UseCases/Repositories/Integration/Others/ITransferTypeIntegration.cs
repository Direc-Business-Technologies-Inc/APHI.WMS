using Application.DataTransferObjects.Transactions.InventoryTransfer.SAP;

namespace Application.UseCases.Repositories.Integration.Others;

public interface ITransferTypeIntegration
{
    Task<IEnumerable<TransferTypeSAPDTO>> GetTransferTypesAsync();
}

