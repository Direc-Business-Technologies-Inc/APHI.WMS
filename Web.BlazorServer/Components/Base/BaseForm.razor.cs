using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Radzen.Blazor;
using System.Reflection;
using Web.BlazorServer.Services.Repositories;

namespace Web.BlazorServer.Components.Base;

public abstract partial class BaseForm<TItem> : BaseComponent, IDisposable where TItem : class, new()
{
    [Inject] protected IFormCacheService FormCacheService { get; set; } = default!;

    public TItem FormData { get; set; } = default!;
    public TItem FormDataClone { get; set; } = default!;
    public RadzenButton SubmitBtn { get; set; } = default!;
    public RadzenTemplateForm<TItem> TemplateForm { get; set; } = default!;
    public EditContext EditContext { get; set; } = default!;

    /// <summary>
    /// Set to false on sensitive forms (login, password management, view-only)
    /// to prevent FormData from being persisted to localStorage.
    /// </summary>
    protected virtual bool EnableFormCache => true;

    /// <summary>
    /// Unique localStorage key for this form's draft cache.
    /// Defaults to the concrete component class name, e.g. "RoleManagementCVU-FCACHE".
    /// Override in Update-mode forms to scope by record: $"{GetType().Name}-{Ref}-FCACHE"
    /// </summary>
    protected virtual string FormCacheKey => $"{GetType().Name}-FCACHE";


    protected override void OnInitialized()
    {
        FormData = new TItem();
        FormDataClone = new TItem();
        EditContext = new EditContext(FormData);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            EditContext.OnFieldChanged += HandleFormChange;

            if (EnableFormCache)
                await TryRestoreFromCacheAsync();
        }
    }

    public virtual void HandleFormChange(object? sender, FieldChangedEventArgs args)
    {
        if (EditContext.IsModified())
        {
            UnsavedChangesService.MarkDirty();
        }

        if (EnableFormCache)
            _ = InvokeAsync(SaveToCacheAsync);
    }

    protected void OnFieldChanged(string propertyName)
    {
        var field = new FieldIdentifier(FormData, propertyName);
        EditContext.NotifyFieldChanged(field);
    }

    public async Task ResetFormContext()
    {
        UnhookFormChangedHandler();

        EditContext = new EditContext(FormData);
        HookFormChangeHandler();

        await InvokeAsync(StateHasChanged);
    }

    void HookFormChangeHandler()
    {
        if (EditContext is not null)
        {
            EditContext.OnFieldChanged += HandleFormChange;
        }
    }

    void UnhookFormChangedHandler()
    {
        if (EditContext is not null)
        {
            EditContext.OnFieldChanged -= HandleFormChange;
        }
    }

    public void NotifyAllFieldsChanged()
    {
        if (EditContext == null || FormData == null)
            return;

        NotifyObjectFields(FormData, string.Empty);

        if (!EditContext.Validate())
            return;
    }

    public void AdaptToClone() => FormDataClone = FormData.Adapt<TItem>();
    public void AdaptToForm() => FormData = FormDataClone.Adapt<TItem>();

    public virtual void Dispose()
    {
        UnhookFormChangedHandler();
    }

    /// <summary>
    /// Clears this form's draft cache from localStorage.
    /// Call from CancelEditing() before navigation and from HandleSubmit() on success.
    /// </summary>
    protected async Task ClearFormCacheAsync()
        => await FormCacheService.ClearAsync(FormCacheKey);

    protected async Task SaveToCacheAsync()
        => await FormCacheService.SaveAsync(FormCacheKey, FormData);

    private async Task TryRestoreFromCacheAsync()
    {
        var cached = await FormCacheService.LoadAsync<TItem>(FormCacheKey);
        if (cached is null)
            return;

        bool restore = await AlertService.PromptAsync(
            message: "You have unsaved form data from a previous session. Would you like to restore it?",
            header: "Restore Draft",
            confirmText: "Restore",
            cancelText: "Discard");

        if (!restore)
        {
            await FormCacheService.ClearAsync(FormCacheKey);
            return;
        }

        FormData = cached.Adapt<TItem>();
        AdaptToClone();
        await ResetFormContext();
        UnsavedChangesService.MarkDirty();
    }

    void NotifyObjectFields(object instance, string parentPath)
    {
        var type = instance.GetType();

        foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (!prop.CanRead || prop.GetIndexParameters().Length > 0)
                continue;

            var fieldPath = string.IsNullOrEmpty(parentPath)
                ? prop.Name
                : $"{parentPath}.{prop.Name}";

            var fieldIdentifier = new FieldIdentifier(FormData, fieldPath);
            EditContext.NotifyFieldChanged(fieldIdentifier);

            var value = prop.GetValue(instance);
            if (value != null &&
                prop.PropertyType.IsClass &&
                prop.PropertyType != typeof(string))
            {
                NotifyObjectFields(value, fieldPath);
            }
        }
    }


    protected abstract Task InitializeEditing();
    protected abstract Task CancelEditing();
    protected abstract Task HandleSubmit();
}
