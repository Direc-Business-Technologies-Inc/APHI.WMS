using Application.DataTransferObjects.Transactions.InventoryCounting;
using Application.UseCases.Repositories.Domain.Transaction.InventoryCounting;
using Database.Libraries.Helpers;
using Database.MsSql.Core;
using Domain.Entities.Administration.User.Management;
using Domain.Entities.Entities.Transaction.InventoryCounting;
using Microsoft.EntityFrameworkCore;
using Shared.Entities;

namespace Database.MsSql.Implementation.Reads;

public class InventoryCountingReadRepo(IDbContextFactory<AppDbContext> dbContextFactory) : AppDbWork<InventoryCountingReadRepo>, IInventoryCountingReadRepo
{
    public Task<(IEnumerable<InventoryCountingDataGridDTO> data, int count)> GetInventoryCountingDataGrid(DataGridIntent intent)
    {
        return ExecuteAppDbWork<(IEnumerable<InventoryCountingDataGridDTO>, int)>(async () =>
        {
            await using var ctx = await dbContextFactory.CreateDbContextAsync();

            var query = from d in ctx.Set<InventoryCountingDocumentDEM>().AsNoTracking()
                        join u in ctx.Set<UserDEM>().AsNoTracking() on d.CreatedBy equals u.Id
                        select new InventoryCountingDataGridDTO
                        {
                            Id = d.Id,
                            AppDocNum = d.LsmsDocNum.Value,
                            Warehouse = d.Warehouse.WhsName,
                            CycleType = d.CycleType,
                            Status = d.Status,
                            CreatedDate = d.CreatedDate,
                            Remarks = d.Remarks,
                            CreatedBy = u.Name.GetFirstLast()
                        };

            var filterPredicate = LinqIntentExpressionBuilder.BuildPredicate<InventoryCountingDataGridDTO>(intent.Filters);
            int count = await query.CountAsync(filterPredicate);

            query = query.Where(filterPredicate);

            foreach (var sort in intent.Sorts)
                query = query.OrderByProperty(sort.Property, sort.Direction);

            query = query.Skip(intent.Skip);
            query = query.Take(intent.Take);

            List<InventoryCountingDataGridDTO> data = await query.ToListAsync();

            return (data, count);
        });
    }

    public Task<InventoryCountingDocumentDTO?> GetInventoryCountingDocument(Guid id)
    {
        return ExecuteAppDbWork<InventoryCountingDocumentDTO?>(async () =>
        {
            await using var ctx = await dbContextFactory.CreateDbContextAsync();

            var dem = await ctx.Set<InventoryCountingDocumentDEM>()
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);

            if (dem == null)
                return null;

            var counterIds = dem.Sheets.Select(s => s.CounterId).Distinct().ToList();
            var counters = await ctx.Set<UserDEM>()
                .AsNoTracking()
                .Where(u => counterIds.Contains(u.Id))
                .ToDictionaryAsync(u => u.Id, u => u.Name.GetFirstLast());

            var dto = new InventoryCountingDocumentDTO
            {
                Id = dem.Id,
                CountingDate = dem.CountingDate,
                CycleType = dem.CycleType,
                Status = dem.Status,
                Remarks = dem.Remarks,
                LsmsDocNum = new() { Value = dem.LsmsDocNum.Value },
                SapReference = dem.SapReference != null ? new() { DocEntry = dem.SapReference.DocEntry ?? 0, DocNum = dem.SapReference.DocNum ?? 0 } : new(),
                Warehouse = new() { WhsCode = dem.Warehouse.WhsCode, WhsName = dem.Warehouse.WhsName },
                DocumentLines = dem.DocumentLines.Select(dl => new InventoryCountingDocumentLineDTO
                {
                    InventoryCountingDocumentId = dl.InventoryCountingDocumentId,
                    ItemCode = dl.ItemCode,
                    ItemName = dl.ItemName,
                    ActualQuantity = dl.ActualQuantity,
                    Quantity = dl.Quantity,
                    UoMCode = dl.UoMCode,
                    UoMValue = dl.UoMValue,
                    UoMName = dl.UoMName,
                    ISBN = dl.ISBN
                }).ToList(),
                Sheets = dem.Sheets.Select(s => new InventoryCountingSheetDTO
                {
                    InventoryCountingDocumentId = s.InventoryCountingDocumentId,
                    SheetNo = new() { Value = s.SheetNo.Value },
                    SubmittedDate = s.SubmittedDate,
                    Status = s.Status,
                    Counter = new()
                    {
                        UserId = s.CounterId,
                        Name = counters.GetValueOrDefault(s.CounterId, "Unknown User")
                    },
                    SheetLines = s.SheetLines.Select(sl => new InventoryCountingSheetLineDTO
                    {
                        SheetNo = sl.SheetNo,
                        ItemCode = sl.ItemCode,
                        ItemName = sl.ItemName,
                        Quantity = sl.Quantity,
                        UoMCode = sl.UoMCode,
                        UoMValue = sl.UoMValue,
                        UoMName = sl.UoMName
                    }).ToList()
                }).ToList()
            };

            return dto;
        });
    }
}
