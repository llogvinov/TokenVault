using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TokenVault.Application.Common.Mapping;

namespace TokenVault.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMappings();
        services.AddMediatR(configuration => 
            configuration.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly));

        return services;
    }
}