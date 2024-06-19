namespace TokenVault.Application.Features.Cryptocurrencies.Common;

public record CryptocurrencyResult(
    Guid Id,
    string Symbol,
    string Name);