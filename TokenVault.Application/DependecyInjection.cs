using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TokenVault.Application.Transactions;

namespace TokenVault.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<TransactionsService>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly));

        return services;
    }
}