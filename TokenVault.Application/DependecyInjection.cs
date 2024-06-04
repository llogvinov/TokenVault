using Microsoft.Extensions.DependencyInjection;
using TokenVault.Application.Authentication;

namespace TokenVault.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        return services;
    }
}