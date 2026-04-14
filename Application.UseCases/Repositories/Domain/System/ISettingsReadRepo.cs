using Application.DataTransferObjects.System.Settings;
using Shared.Entities;

namespace Application.UseCases.Repositories.Domain.System;

public interface ISettingsReadRepo
{
    Task<(IEnumerable<SettingsDataGridDTO> Data, int Count)> GetSettingsTableDetailsAsync(DataGridIntent intent);
}
