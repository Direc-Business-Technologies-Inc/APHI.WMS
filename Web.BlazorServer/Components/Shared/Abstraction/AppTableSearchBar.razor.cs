using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Shared.Libraries.Entities;
using Web.BlazorServer.Components.Base;

namespace Web.BlazorServer.Components.Shared.Abstraction;

public partial class AppTableSearchBar<TItem> : BaseComponent where TItem : class
{
    [Parameter] public IEnumerable<string> Properties { get; set; } = [];
    [Parameter] public AppFilterDescriptor? SearchFilter { get; set; }
    [Parameter] public EventCallback<AppFilterDescriptor?> SearchFilterChanged { get; set; }
    [Parameter] public EventCallback OnSearch { get; set; }
    [Parameter] public string Placeholder { get; set; } = "Search...";
    [Parameter] public int DebounceMs { get; set; } = 400;

    private string _searchValue = string.Empty;
    private CancellationTokenSource? _debounceCts;

    private async Task OnInputChanged(ChangeEventArgs args)
    {
        _searchValue = args.Value?.ToString() ?? string.Empty;
        await DebounceSearchAsync();
    }

    private async Task OnKeyDownAsync(KeyboardEventArgs args)
    {
        if (args.Key == "Enter")
        {
            _debounceCts?.Cancel();
            await ExecuteSearchAsync();
        }
    }

    private async Task ClearSearchAsync()
    {
        _searchValue = string.Empty;
        _debounceCts?.Cancel();
        await ExecuteSearchAsync();
    }

    private async Task DebounceSearchAsync()
    {
        _debounceCts?.Cancel();
        _debounceCts = new CancellationTokenSource();

        try
        {
            await Task.Delay(DebounceMs, _debounceCts.Token);
            await ExecuteSearchAsync();
        }
        catch (TaskCanceledException)
        {
            // Ignore
        }
    }

    private async Task ExecuteSearchAsync()
    {
        var newFilter = BuildFilter();
        if (newFilter != SearchFilter)
        {
            SearchFilter = newFilter;
            await SearchFilterChanged.InvokeAsync(SearchFilter);
            await OnSearch.InvokeAsync();
        }
    }

    private AppFilterDescriptor? BuildFilter()
    {
        if (string.IsNullOrWhiteSpace(_searchValue))
            return null;

        var leaves = new List<AppFilterDescriptor>();
        var itemType = typeof(TItem);

        foreach (var propName in Properties)
        {
            var propInfo = itemType.GetProperty(propName);
            if (propInfo == null) continue;

            var targetType = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;

            if (targetType == typeof(string))
            {
                leaves.Add(new AppFilterDescriptor
                {
                    Property = propName,
                    Value = _searchValue,
                    ComparisonOperator = ComparisonOperatorEnum.Contains,
                    FilterValueType = FilterValueTypeEnum.String
                });
            }
            else if (targetType.IsNumericType())
            {
                if (decimal.TryParse(_searchValue, out var decimalValue))
                {
                    leaves.Add(new AppFilterDescriptor
                    {
                        Property = propName,
                        Value = decimalValue,
                        ComparisonOperator = ComparisonOperatorEnum.Equals,
                        FilterValueType = FilterValueTypeEnum.Number
                    });
                }
            }
            else if (targetType == typeof(DateTime))
            {
                if (DateTime.TryParse(_searchValue, out var dateValue))
                {
                    leaves.Add(new AppFilterDescriptor
                    {
                        Property = propName,
                        Value = dateValue,
                        ComparisonOperator = ComparisonOperatorEnum.Equals,
                        FilterValueType = FilterValueTypeEnum.DateTime
                    });
                }
            }
            else if (targetType == typeof(DateOnly))
            {
                if (DateOnly.TryParse(_searchValue, out var dateValue))
                {
                    leaves.Add(new AppFilterDescriptor
                    {
                        Property = propName,
                        Value = dateValue,
                        ComparisonOperator = ComparisonOperatorEnum.Equals,
                        FilterValueType = FilterValueTypeEnum.DateOnly
                    });
                }
            }
            else if (targetType.IsEnum)
            {
                if (Enum.TryParse(targetType, _searchValue, true, out var enumValue) && Enum.IsDefined(targetType, enumValue!))
                {
                    leaves.Add(new AppFilterDescriptor
                    {
                        Property = propName,
                        Value = (int)enumValue!,
                        ComparisonOperator = ComparisonOperatorEnum.Equals,
                        FilterValueType = FilterValueTypeEnum.Number
                    });
                }
            }
        }

        if (leaves.Count == 0)
            return null;

        return new AppFilterDescriptor
        {
            LogicalOperator = LogicalOperatorEnum.OR,
            Filters = leaves
        };
    }
}
