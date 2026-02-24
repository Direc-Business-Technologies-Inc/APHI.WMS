using Microsoft.Extensions.DependencyInjection.Extensions;
using Web.BlazorServer.Handlers.Implementations.Administration.Authorization;
using Web.BlazorServer.Handlers.Implementations.Administration.Role;
using Web.BlazorServer.Handlers.Implementations.Administration.User;
using Web.BlazorServer.Handlers.Implementations.Others;
using Web.BlazorServer.Handlers.Implementations.System;
using Web.BlazorServer.Handlers.Implementations.Transaction.GoodsIssue;
using Web.BlazorServer.Handlers.Implementations.Transaction.GoodsReceipt;
using Web.BlazorServer.Handlers.Implementations.Transaction.GoodsReturn;
using Web.BlazorServer.Handlers.Implementations.Transaction.Receiving;
using Web.BlazorServer.Handlers.Repositories.Administration.Authorization;
using Web.BlazorServer.Handlers.Repositories.Administration.Role;
using Web.BlazorServer.Handlers.Repositories.Administration.User;
using Web.BlazorServer.Handlers.Repositories.Others;
using Web.BlazorServer.Handlers.Repositories.System;
using Web.BlazorServer.Handlers.Repositories.Transaction.GoodsIssue;
using Web.BlazorServer.Handlers.Repositories.Transaction.GoodsReceipt;
using Web.BlazorServer.Handlers.Repositories.Transaction.GoodsReturn;
using Web.BlazorServer.Handlers.Repositories.Transaction.Receiving;

namespace Web.BlazorServer.Handlers;

public static class BlazorServerHandlersDI
{
    public static IServiceCollection AddBlazorServerHandlers(this IServiceCollection services)
    {
        services.TryAddTransient<INavigationRouteHandler, NavigationRouteHandler>();

        services.TryAddTransient<IUserManagementHandler, UserManagementHandler>();
        services.TryAddTransient<IRoleManagementHandler, RoleManagementHandler>();
        services.TryAddTransient<IModuleHandler, ModuleHandler>();
        services.TryAddTransient<IDocumentNumberHandler, DocumentNumberHandler>();
        services.TryAddTransient<IAuthorizationHandler, AuthorizationHandler>();
        services.TryAddTransient<IReceivingHandler, ReceivingHandler>();
        services.TryAddTransient<IGoodsReturnHandler, GoodsReturnHandler>();
        services.TryAddTransient<IBusinessPartnerHandler, BusinessPartnerHandler>();
        services.TryAddTransient<IItemMasterDataHandler, ItemMasterDataHandler>();
        services.TryAddTransient<IWarehouseMasterDataHandler, WarehouseMasterDataHandler>();
        services.TryAddTransient<IGoodsReceiptHandler, GoodsReceiptHandler>();
        services.TryAddTransient<IGoodsIssueHandler, GoodsIssueHandler>();
        services.TryAddTransient<ITransactionTypeHandler, TransactionTypeHandler>();

        return services;
    }
}
