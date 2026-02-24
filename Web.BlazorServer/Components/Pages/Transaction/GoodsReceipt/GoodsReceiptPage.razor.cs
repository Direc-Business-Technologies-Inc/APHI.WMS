using Microsoft.AspNetCore.Components;

namespace Web.BlazorServer.Components.Pages.Transaction.GoodsReceipt;

public partial class GoodsReceiptPage
{
    #region Parameters
    [SupplyParameterFromQuery]
    [Parameter] public string T { get; set; } = "aprvd";
    #endregion Parameters

    #region Primitives
    int SelectedTab { get; set; } = 0;
    #endregion Primitives

    #region Overrides

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        if (T is not null)
            SelectedTab = T.ToLower() == "aprvd" ? 0 : T.ToLower() == "pndng" ? 1 : 2;
    }

    #endregion Overrides

    #region Custom Functions
    void TabChanged()
    {
        T = SelectedTab == 0 ? "aprvd" : SelectedTab == 1 ? "pndng" : "rjct";
        NavManager.NavigateTo($"/transactions/inventory/goods-receipt?T={T}");
    }
    #endregion Custom Functions
}
