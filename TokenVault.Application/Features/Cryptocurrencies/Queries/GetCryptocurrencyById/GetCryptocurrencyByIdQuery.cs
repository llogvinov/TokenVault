using MediatR;
using TokenVault.Application.Features.Cryptocurrencies.Common;

namespace TokenVault.Application.Features.Cryptocurrencies.Queries.GetCryptocurrencyById;

public record GetCryptocurrencyByIdQuery(
    Guid CryptocurrencyId) : IRequest<CryptocurrencyResult>;