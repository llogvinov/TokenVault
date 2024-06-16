namespace TokenVault.Contracts.Cryptocurrency;

public record AddCryptocurrencyRequest(
    string Symbol,
    string Name);