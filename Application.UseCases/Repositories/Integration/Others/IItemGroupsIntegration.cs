using Application.DataTransferObjects.Others;
using Application.DataTransferObjects.Others.SAP;
using Shared.Entities;

namespace Application.UseCases.Repositories.Integration.Others;

public interface IItemGroupsIntegration
{
    Task<(IEnumerable<ItemGroupSAPDTO> Data, int Count)> GetItemGroupsAsync(DataGridIntent intent);
}
