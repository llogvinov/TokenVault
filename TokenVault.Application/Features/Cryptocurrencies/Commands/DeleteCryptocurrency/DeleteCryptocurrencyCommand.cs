using MediatR;
using TokenVault.Contracts.Cryptocurrency;

namespace TokenVault.Application.Features.Cryptocurrencies.Commands.DeleteCryptocurrency;

public record DeleteCryptocurrencyCommand(
    Guid CryptocurrencyId) : IRequest<CryptocurrencyResponse>;