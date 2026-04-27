using Domain.Entities.Enums.Transaction.InventoryCounting;
using Mapster;
using Microsoft.AspNetCore.Components;
using Shared.Libraries.Kernel;
using Web.BlazorServer.Components.Base;
using Web.BlazorServer.Defaults;
using Web.BlazorServer.Handlers.Repositories.Administration.Settings;
using Web.BlazorServer.ViewModels.Administration.Settings;

namespace Web.BlazorServer.Components.Pages.Administrator.Settings;

public partial class ApplicationSettingsPage : BaseComponent
{
    record CycleTypeOption(string Value, string Label);

    #region Injects
    [Inject] ISettingsHandler SettingsHandler { get; set; } = default!;
    #endregion Injects

    #region Primitives
    string ActionGetAllSettings { get; } = EnumHelper.GetEnumDescription(AppActions.GetAllSettings);
    string ActionUpdateSettings { get; } = EnumHelper.GetEnumDescription(AppActions.UpdateSettingsList);

    bool IsLoadingBusy => AppBusyService.IsBusy(ActionGetAllSettings);
    bool IsSavingBusy => AppBusyService.IsBusy(ActionUpdateSettings);
    bool IsAnyBusy => IsLoadingBusy || IsSavingBusy;

    bool IsEditing { get; set; } = false;
    #endregion Primitives

    #region Data Structures
    List<SettingsVM> Settings { get; set; } = [];
    List<SettingsVM> SettingsClone { get; set; } = [];

    IEnumerable<CycleTypeOption> CycleTypeOptions { get; } =
    [
        new("None", "None"),
        ..Enum.GetValues<CycleType>()
              .Select(c => new CycleTypeOption(c.ToString(), EnumHelper.GetEnumDescription(c)))
    ];

    SettingsVM? InventoryCountingPostingCycleSetting =>
        Settings.FirstOrDefault(x => x.Name == "Inventory Counting Posting Cycle");

    SettingsVM? MaxFailedLoginAttemptsSetting =>
        Settings.FirstOrDefault(x => x.Name == "Max Failed Login Attempts");

    int MaxFailedLoginAttempts
    {
        get => int.TryParse(MaxFailedLoginAttemptsSetting?.Value, out var v) ? v : 5;
        set { if (MaxFailedLoginAttemptsSetting is not null) MaxFailedLoginAttemptsSetting.Value = value.ToString(); }
    }
    #endregion Data Structures

    #region Overrides
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await LoadSettingsAsync();
    }
    #endregion Overrides

    #region Data Loading
    async Task LoadSettingsAsync()
    {
        var action = await AppActionFactory.RunAsync(async () =>
        {
            AppBusyService.SetBusy(ActionGetAllSettings, true);
            return await SettingsHandler.GetAllSettingsAsync();

        }, AppActionOptionPresets.Loading(ActionGetAllSettings));

        AppBusyService.SetBusy(ActionGetAllSettings, false);

        action.OnSuccess(result =>
        {
            Settings = result?.ToList() ?? [];
            return Task.CompletedTask;
        });
    }
    #endregion Data Loading

    #region Edit / Cancel / Save
    async Task OnEdit()
    {
        SettingsClone = Settings.Adapt<List<SettingsVM>>();
        IsEditing = true;
        await InvokeAsync(StateHasChanged);
    }

    async Task CancelEdit()
    {
        if (UnsavedChangesService.HasChanges)
        {
            if (!await AlertService.HasUnsavedChangesAsync(header: "Cancel Settings Edit"))
                return;

            UnsavedChangesService.MarkClean();
        }

        Settings = SettingsClone.Adapt<List<SettingsVM>>();
        IsEditing = false;
        await InvokeAsync(StateHasChanged);
    }

    async Task SaveChanges()
    {
        var action = await AppActionFactory.RunAsync(async () =>
        {
            AppBusyService.SetBusy(ActionUpdateSettings, true);
            return await SettingsHandler.UpdateSettingsListAsync(Settings);

        }, AppActionOptionPresets.Confirmed(ActionUpdateSettings));

        AppBusyService.SetBusy(ActionUpdateSettings, false);

        action.OnSuccess(_ =>
        {
            UnsavedChangesService.MarkClean();
            SettingsClone = Settings.Adapt<List<SettingsVM>>();
            IsEditing = false;
            InvokeAsync(StateHasChanged);
            return Task.CompletedTask;
        });
    }
    #endregion Edit / Cancel / Save
}
