using Mapster;
using TokenVault.Application.Authentication.Commands.Register;
using TokenVault.Application.Authentication.Common;
using TokenVault.Application.Authentication.Queries.Login;
using TokenVault.Contracts.Authentication;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();

        config.NewConfig<(RegisterCommand command, string password), User>()
            .Map(dest => dest.Password, src => src.password)
            .Map(dest => dest, src => src.command);

        config.NewConfig<LoginRequest, LoginQuery>();

        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest, src => src.User);
    }
}