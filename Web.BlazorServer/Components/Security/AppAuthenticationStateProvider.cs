using Microsoft.AspNetCore.Components.Authorization;

namespace Web.BlazorServer.Components.Security;

public class AppAuthenticationStateProvider : AuthenticationStateProvider
{
    AuthenticationState authenticationState { get; set; }

    public AppAuthenticationStateProvider(AppAuthenticationService service)
    {
        authenticationState = new AuthenticationState(service.CurrentUser);

        service.UserChanged += (newUser) =>
        {
            authenticationState = new AuthenticationState(newUser);

            NotifyAuthenticationStateChanged(
                Task.FromResult(new AuthenticationState(newUser)));
        };
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync() =>
        Task.FromResult(authenticationState);

    public void NotifyAuthenticationStateChanged() => NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
}
