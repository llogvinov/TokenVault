using MediatR;
using TokenVault.Contracts.Cryptocurrency;

namespace TokenVault.Application.Features.Cryptocurrencies.Commands.CreateCryptocurrency;

public record CreateCryptocurrencyCommand(
    string Symbol,
    string Name) : IRequest<CreateCryptocurrencyResponse>;