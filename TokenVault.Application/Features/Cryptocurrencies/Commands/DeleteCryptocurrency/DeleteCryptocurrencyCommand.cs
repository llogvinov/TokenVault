using MediatR;
using TokenVault.Application.Features.Cryptocurrencies.Common;

namespace TokenVault.Application.Features.Cryptocurrencies.Commands.DeleteCryptocurrency;

public record DeleteCryptocurrencyCommand(
    Guid CryptocurrencyId) : IRequest<CryptocurrencyResult>;