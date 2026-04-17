using Application.DataTransferObjects.System.Settings;
using Shared.Libraries.Entities;

namespace Application.UseCases.Repositories.Domain.System;

public interface ISettingsReadRepo
{
    Task<(IEnumerable<SettingsDataGridDTO> Data, int Count)> GetSettingsTableDetailsAsync(DataGridIntent intent);
}
