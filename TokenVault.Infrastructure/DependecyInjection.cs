using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using TokenVault.Application.Common.Interfaces.Authentication;
using TokenVault.Application.Common.Interfaces.Services;
using TokenVault.Infrastructure.Authentication;
using TokenVault.Infrastructure.Services;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Infrastructure.Persistence;

namespace TokenVault.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}