using System.ComponentModel;

namespace Web.BlazorServer.Components.Pages.Administrator.Authorization;

public partial class AuthorizationPage
{

    #region Primitives
    AuthorizationType ActiveTab { get; set; } = AuthorizationType.Role;
    #endregion Primitives

    #region Overrides

    #endregion Overrides

}

public enum AuthorizationType
{
    [Description("Role Authorization")]
    Role,
    [Description("User Authorization")]
    User
}
