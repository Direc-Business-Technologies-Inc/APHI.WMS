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
    int DataVersion { get; set; } = 0;
    #endregion Primitives

    #region Data Structures
    List<SettingsVM> Settings { get; set; } = [];
    List<SettingsVM> SettingsClone { get; set; } = [];
    Dictionary<string, object> SettingsData { get; set; } = [];
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
            SettingsData = BuildSettingsData(Settings);
            DataVersion++;
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
        SettingsData = BuildSettingsData(Settings);
        DataVersion++;
        IsEditing = false;
        await InvokeAsync(StateHasChanged);
    }

    async Task SaveChanges()
    {
        SyncSettingsFromData();

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

    #region Helpers
    // Converts SettingsVM list to a typed dictionary for DynamicInput.
    Dictionary<string, object> BuildSettingsData(List<SettingsVM> settings)
    {
        var dict = new Dictionary<string, object>();

        foreach (var s in settings)
        {
            try { dict[s.Name] = AppTypeConverter.Convert(s.Value, s.Type); }
            catch { dict[s.Name] = s.Value; }
        }

        return dict;
    }

    // Writes typed dictionary values back into SettingsVM.Value strings before saving.
    void SyncSettingsFromData()
    {
        foreach (var setting in Settings)
        {
            if (SettingsData.TryGetValue(setting.Name, out var value))
                setting.Value = value?.ToString() ?? setting.Value;
        }
    }

    // Resolves System.Type from AppTypes enum for the DynamicInput Type parameter.
    Type GetCSharpType(SettingsVM setting) => AppTypeConverter.GetCSharpType(setting.Type);
    #endregion Helpers
}
