namespace TokenVault.Contracts.Cryptocurrency;

public record CryptocurrencyResponse(
    Guid Id,
    string Symbol,
    string Name);