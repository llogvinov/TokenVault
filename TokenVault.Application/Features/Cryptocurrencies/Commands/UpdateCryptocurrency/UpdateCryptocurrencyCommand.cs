using MediatR;
using TokenVault.Application.Features.Cryptocurrencies.Common;
using TokenVault.Contracts.Cryptocurrency;

namespace TokenVault.Application.Features.Cryptocurrencies.Commands.UpdateCryptocurrency;

public record UpdateCryptocurrencyCommand(
    Guid CryptocurrencyId,
    UpdateCryptocurrencyRequest Request) : IRequest<CryptocurrencyResult>;