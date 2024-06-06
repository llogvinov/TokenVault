using MediatR;
using TokenVault.Api.Common.Mapping;

namespace TokenVault.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddMappings();

        return services;
    }
}