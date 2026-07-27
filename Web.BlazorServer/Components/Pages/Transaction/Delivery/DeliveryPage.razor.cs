using Microsoft.AspNetCore.Components;

namespace Web.BlazorServer.Components.Pages.Transaction.Delivery;

public partial class DeliveryPage
{
    #region Parameters
    [SupplyParameterFromQuery]
    [Parameter] public string T { get; set; } = "so";
    #endregion Parameters

    #region Primitives
    int SelectedTab { get; set; } = 0;
    #endregion Primitives

    #region Overrides
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        if (T is not null)
            SelectedTab = T.ToLower() switch
            {
                "salesorder" => 0,
                "delivery" => 1,
                "deliverydrafts" => 2,
                _ => 0
            };
    }
    #endregion Overrides

    #region Custom Functions
    void TabChanged()
    {
        T = SelectedTab switch
        {
            1 => "delivery",
            2 => "deliverydrafts",
            _ => "salesorder"
        };
        NavManager.NavigateTo($"/transactions/sales/delivery?T={T}");
    }
    #endregion Custom Functions
}
