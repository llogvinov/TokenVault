using MediatR;
using TokenVault.Application.Features.Cryptocurrencies.Common;

namespace TokenVault.Application.Features.Cryptocurrencies.Commands.CreateCryptocurrency;

public record CreateCryptocurrencyCommand(
    string Symbol,
    string Name) : IRequest<CryptocurrencyResult>;