using Microsoft.AspNetCore.Components;

namespace Web.BlazorServer.Components.Pages.Transaction.Receiving;

public partial class ReceivingPage
{
    #region Parameters
    [SupplyParameterFromQuery]
    [Parameter] public string T { get; set; } = "po";
    #endregion Parameters

    #region Primitives
    int SelectedTab { get; set; } = 0;
    #endregion Primitives

    #region Custom Functions
    void TabChanged() => T = SelectedTab == 0 ? "po" : "grpo";
    #endregion Custom Functions
}
