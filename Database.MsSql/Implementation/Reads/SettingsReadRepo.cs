using Application.DataTransferObjects.System.Settings;
using Application.UseCases.Repositories.Domain.System;
using Database.Libraries.Helpers;
using Database.MsSql.Core;
using Domain.Entities.Entities.System;
using Microsoft.EntityFrameworkCore;
using Shared.Libraries.Entities;
using Shared.Libraries.Kernel;

namespace Database.MsSql.Implementation.Reads;

public class SettingsReadRepo(IDbContextFactory<AppDbContext> dbContextFactory) : AppDbWork<SettingsReadRepo>, ISettingsReadRepo
{
    public Task<(IEnumerable<SettingsDataGridDTO> Data, int Count)> GetSettingsTableDetailsAsync(DataGridIntent intent)
    {
        return ExecuteAppDbWork<(IEnumerable<SettingsDataGridDTO>, int)>(async () =>
        {
            await using var ctx = await dbContextFactory.CreateDbContextAsync();

            var query = ctx.Set<SettingsDEM>().AsNoTracking();

            var filterPredicate = LinqIntentExpressionBuilder.BuildPredicate<SettingsDEM>(intent.Filters);
            int count = await query.CountAsync(filterPredicate);

            query = query.Where(filterPredicate);

            foreach (var sort in intent.Sorts)
                query = query.OrderByProperty(sort.Property, sort.Direction);

            query = query.Skip(intent.Skip);
            query = query.Take(intent.Take);

            List<SettingsDEM> rawData = await query.ToListAsync();

            List<SettingsDataGridDTO> data = rawData.Select(s => new SettingsDataGridDTO
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Type = EnumHelper.ParseStringToEnum<AppTypes>(s.Type),
                Value = s.Value
            }).ToList();

            return (data, count);
        });
    }
}
