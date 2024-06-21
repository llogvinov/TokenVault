using Mapster;
using TokenVault.Application.Features.Cryptocurrencies.Commands.CreateCryptocurrency;
using TokenVault.Application.Features.Cryptocurrencies.Commands.UpdateCryptocurrency;
using TokenVault.Application.Features.Cryptocurrencies.Common;
using TokenVault.Contracts.Cryptocurrency;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Common.Mapping;

public class CryptocurrenciesMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateCryptocurrencyRequest, CreateCryptocurrencyCommand>();

        config.NewConfig<UpdateCryptocurrencyCommand, UpdateCryptocurrencyDetails>()
            .Map(dest => dest, src => src.Request);

        config.NewConfig<CreateCryptocurrencyCommand, Cryptocurrency>();

        config.NewConfig<Cryptocurrency, CryptocurrencyResult>();

        config.NewConfig<CryptocurrencyResult, CryptocurrencyResponse>();
    }
}