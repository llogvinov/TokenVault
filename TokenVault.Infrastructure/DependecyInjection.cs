using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using TokenVault.Application.Common.Interfaces.Authentication;
using TokenVault.Application.Common.Interfaces.Services;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Infrastructure.Authentication;
using TokenVault.Infrastructure.Services;
using TokenVault.Infrastructure.Persistence;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TokenVault.Infrastructure.Persistence.Repository;

namespace TokenVault.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddAuth(configuration)
            .AddPersistence();
        
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddHttpClient<ICryptocurrencyPriceProvider, SimpleAPICryptocurrencyPriceProvider>();
        services.AddSingleton<ICryptocurrencyPriceProvider, SimpleAPICryptocurrencyPriceProvider>();

        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext<TokenVaultDbContext>(options =>
            options.UseSqlServer(DbConnection.ConnectionString));
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    public static IServiceCollection AddAuth(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.MapInboundClaims = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret))
                };
            });

        return services;
    }
}