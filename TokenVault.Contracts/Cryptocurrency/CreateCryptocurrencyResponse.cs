namespace TokenVault.Contracts.Cryptocurrency;

public record CreateCryptocurrencyResponse(
    Guid Id,
    string Symbol,
    string Name);