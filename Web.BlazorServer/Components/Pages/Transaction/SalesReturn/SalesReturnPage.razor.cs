using Microsoft.AspNetCore.Components;

namespace Web.BlazorServer.Components.Pages.Transaction.SalesReturn;

public partial class SalesReturnPage
{
    #region Parameters
    [SupplyParameterFromQuery]
    [Parameter] public string T { get; set; } = "sr";
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
                "salesreturn" => 0,
                "salesreturndraft" => 1,
                "salesreturnrequest" => 2,
                _ => 0
            };
    }

    #endregion Overrides

    #region Custom Functions
    void TabChanged()
    {
        T = SelectedTab switch
        {
            1 => "salesreturndraft",
            2 => "salesreturnrequest",
            _ => "salesreturn"
        };

        NavManager.NavigateTo($"/transactions/sales/sales-return?T={T}");
    }
    #endregion Custom Functions
}
