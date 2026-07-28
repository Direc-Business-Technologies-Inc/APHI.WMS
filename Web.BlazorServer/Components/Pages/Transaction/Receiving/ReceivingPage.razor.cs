using Microsoft.AspNetCore.Components;
using Shared.Libraries.Kernel;
using Web.BlazorServer.Defaults;

namespace Web.BlazorServer.Components.Pages.Transaction.Receiving;

public partial class ReceivingPage
{
    #region Parameters
    [SupplyParameterFromQuery]
    [Parameter] public string T { get; set; } = "po";
    #endregion Parameters

    #region Primitives
    int SelectedTab { get; set; } = 0;
    string ActionGetPurchaseOrders { get; } = EnumHelper.GetEnumDescription(AppActions.GetAllPurchaseOrders);

    #endregion Primitives

    #region Overrides

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        if (T is not null)
            SelectedTab = T.ToLower() switch
            {
                "grpo" => 1,
                "grpodrafts" => 2,
                _ => 0
            };
    }

    #endregion Overrides

    #region Custom Functions
    void TabChanged()
    {
        T = SelectedTab switch
        {
            1 => "grpo",
            2 => "grpodrafts",
            _ => "po"
        };
        NavManager.NavigateTo($"/transactions/purchasing/receiving?T={T}");
    }
    #endregion Custom Functions
}
