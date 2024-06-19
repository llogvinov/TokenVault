namespace TokenVault.Contracts.Cryptocurrency;

public record CreateCryptocurrencyRequest(
    string Symbol,
    string Name);