using Application.UseCases.Repositories.Integration.Others;
using Application.UseCases.Repositories.Integration.Transaction.Delivery;
using Application.UseCases.Repositories.Integration.Transaction.GoodsIssue;
using Application.UseCases.Repositories.Integration.Transaction.GoodsReceipt;
using Application.UseCases.Repositories.Integration.Transaction.GoodsReturn;
using Application.UseCases.Repositories.Integration.Transaction.Receiving;
using Application.UseCases.Repositories.Integration.Transaction.SalesReturn;
using Integration.SAP.Implementations.Others;
using Integration.SAP.Implementations.Transaction.Delivery;
using Integration.SAP.Implementations.Transaction.GoodsIssue;
using Integration.SAP.Implementations.Transaction.GoodsReceipt;
using Integration.SAP.Implementations.Transaction.GoodsReturn;
using Integration.SAP.Implementations.Transaction.Receiving;
using Integration.SAP.Implementations.Transaction.SalesReturn;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Integration.SAP.Implementations;

public static class SAPImplementationsDI
{
    public static IServiceCollection AddSAPImplementationsIntegraton(this IServiceCollection services)
    {
        services.TryAddTransient<IReceivingIntegration, ReceivingIntegration>();
        services.TryAddTransient<IGoodsReturnIntegration, GoodsReturnIntegration>();
        services.TryAddTransient<IBusinessPartnerIntegration, BusinessPartnerIntegration>();
        services.TryAddTransient<IItemMasterDataIntegration, ItemMasterDataIntegration>();
        services.TryAddTransient<IWarehouseMasterDataIntegration, WarehouseMasterDataIntegration>();
        services.TryAddTransient<ITransactionTypeIntegration, TransactionTypeIntegration>();
        services.TryAddTransient<IGoodsIssueIntegration, GoodsIssueIntegration>();
        services.TryAddTransient<IGoodsReceiptIntegration, GoodsReceiptIntegration>();
        services.TryAddTransient<ISchoolYearIntegration, SchoolYearIntegration>();
        services.TryAddTransient<IDeliveryIntegration, DeliveryIntegration>();
        services.TryAddTransient<ISalesReturnIntegration, SalesReturnIntegration>();

        return services;
    }
}
