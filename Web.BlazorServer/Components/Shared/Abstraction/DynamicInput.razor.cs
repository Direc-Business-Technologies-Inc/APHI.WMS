using Microsoft.AspNetCore.Components;
using Web.BlazorServer.Components.Base;

namespace Web.BlazorServer.Components.Shared.Abstraction;

public partial class DynamicInput : BaseComponent
{
    #region Parameters
    [Parameter] public required Type Type { get; set; }
    [Parameter] public required string Key { get; set; }
    [Parameter] public required IDictionary<string, object> Data { get; set; }
    [Parameter] public string? Description { get; set; }
    [Parameter] public bool Disabled { get; set; }
    [Parameter] public EventCallback<IDictionary<string, object>> DataChanged { get; set; }
    #endregion Parameters

    #region Local State
    string StringValue { get; set; } = string.Empty;
    int IntValue { get; set; }
    decimal DecimalValue { get; set; }
    bool BoolValue { get; set; }
    DateTime DateTimeValue { get; set; } = DateTime.Now;
    #endregion Local State

    #region Overrides
    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (!Data.TryGetValue(Key, out var raw))
            return;

        string rawStr = raw?.ToString() ?? string.Empty;

        if      (Type == typeof(string))   StringValue   = rawStr;
        else if (Type == typeof(int))      IntValue      = int.TryParse(rawStr, out int i)         ? i  : 0;
        else if (Type == typeof(decimal))  DecimalValue  = decimal.TryParse(rawStr, out decimal d) ? d  : 0;
        else if (Type == typeof(bool))     BoolValue     = bool.TryParse(rawStr, out bool b)   && b;
        else if (Type == typeof(DateTime)) DateTimeValue = DateTime.TryParse(rawStr, out DateTime dt) ? dt : DateTime.Now;
        else                               StringValue   = rawStr;
    }
    #endregion Overrides

    #region Save
    async Task Save()
    {
        UnsavedChangesService.MarkDirty();

        if      (Type == typeof(string))   Data[Key] = StringValue;
        else if (Type == typeof(int))      Data[Key] = IntValue;
        else if (Type == typeof(decimal))  Data[Key] = DecimalValue;
        else if (Type == typeof(bool))     Data[Key] = BoolValue;
        else if (Type == typeof(DateTime)) Data[Key] = DateTimeValue;
        else                               Data[Key] = StringValue;

        await DataChanged.InvokeAsync(Data);
    }
    #endregion Save
}
