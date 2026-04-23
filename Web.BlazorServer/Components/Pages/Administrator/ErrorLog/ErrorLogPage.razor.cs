using Microsoft.AspNetCore.Components;
using Radzen;
using Shared.Libraries.Kernel;
using Web.BlazorServer.Components.Pages.Administrator.ErrorLog.Components;
using Web.BlazorServer.Defaults;
using Web.BlazorServer.Services.Repositories;
using Web.BlazorServer.ViewModels.Administration.ErrorLog;

namespace Web.BlazorServer.Components.Pages.Administrator.ErrorLog;

public partial class ErrorLogPage
{
    [Inject] IAppErrorLogger ErrorLogger { get; set; } = default!;

    List<ErrorLogVM> LogFiles { get; set; } = [];

    readonly string ActionGetLogs = EnumHelper.GetEnumDescription(AppActions.GetErrorLogs);
    readonly string ActionDeleteLog = EnumHelper.GetEnumDescription(AppActions.DeleteErrorLog);
    readonly string ActionDeleteAll = EnumHelper.GetEnumDescription(AppActions.DeleteAllErrorLogs);

    protected override async Task OnInitializedAsync()
    {
        await LoadLogsAsync();
    }

    async Task LoadLogsAsync()
    {
        var action = await AppActionFactory.RunAsync(async () =>
        {
            AppBusyService.SetBusy(ActionGetLogs, true);
            await Task.CompletedTask;

            return ErrorLogger.GetLogFiles()
                .Select(f => new ErrorLogVM
                {
                    FileName = f.Name,
                    FileSize = f.Length,
                    LastModified = f.LastWriteTime
                })
                .ToList();

        }, AppActionOptionPresets.Loading(ActionGetLogs));

        AppBusyService.SetBusy(ActionGetLogs, false);
        action.OnSuccess(result => { LogFiles = result ?? []; return Task.CompletedTask; });
        StateHasChanged();
    }

    async Task ViewLogAsync(ErrorLogVM log)
    {
        var content = await ErrorLogger.ReadLogFileAsync(log.FileName);
        await DialogService.OpenAsync<ErrorLogViewerDialog>(
            log.FileName,
            new Dictionary<string, object> { [nameof(ErrorLogViewerDialog.Content)] = content },
            new DialogOptions { Width = "900px", Height = "700px", Resizable = true, Draggable = true }
        );
    }

    async Task DeleteLogAsync(ErrorLogVM log)
    {
        var action = await AppActionFactory.RunAsync(async () =>
        {
            AppBusyService.SetBusy(ActionDeleteLog, true);
            await Task.CompletedTask;
            ErrorLogger.DeleteLogFile(log.FileName);

        }, AppActionOptionPresets.Confirmed(ActionDeleteLog));

        AppBusyService.SetBusy(ActionDeleteLog, false);
        action.OnSuccess(async () => await LoadLogsAsync());
    }

    async Task DeleteAllAsync()
    {
        var action = await AppActionFactory.RunAsync(async () =>
        {
            AppBusyService.SetBusy(ActionDeleteAll, true);
            await Task.CompletedTask;
            ErrorLogger.DeleteAllLogFiles();

        }, AppActionOptionPresets.Confirmed(ActionDeleteAll));

        AppBusyService.SetBusy(ActionDeleteAll, false);
        action.OnSuccess(async () => await LoadLogsAsync());
    }

    async Task RefreshAsync() => await LoadLogsAsync();
}
