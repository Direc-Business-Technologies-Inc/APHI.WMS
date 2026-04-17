using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shared.Libraries.Services.Implementation;
using Shared.Libraries.Services.Repository;

namespace Shared.Libraries.Services;

public static class SharedServicesDI
{
    public static IServiceCollection AddSharedServices(this IServiceCollection services)
    {

        services.TryAddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }
}
