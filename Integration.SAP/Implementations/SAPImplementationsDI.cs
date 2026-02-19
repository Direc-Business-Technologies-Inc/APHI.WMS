using Application.UseCases.Repositories.Integration.Transaction.Receiving;
using Integration.SAP.Implementations.Transaction.Receiving;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Integration.SAP.Implementations;

public static class SAPImplementationsDI
{
    public static IServiceCollection AddSAPImplementationsIntegraton(this IServiceCollection services)
    {
        services.TryAddTransient<IReceivingIntegration, ReceivingIntegration>();

        return services;
    }
}
