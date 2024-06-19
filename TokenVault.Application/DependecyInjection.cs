using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TokenVault.Application.Assets;
using TokenVault.Application.Common.Mapping;
using TokenVault.Application.Features.Potfolios;
using TokenVault.Application.Transactions;

namespace TokenVault.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddApplicationServices();
        services.AddMappings();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly));

        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<TransactionsService>();
        services.AddScoped<AssetsService>();

        return services;
    }
}