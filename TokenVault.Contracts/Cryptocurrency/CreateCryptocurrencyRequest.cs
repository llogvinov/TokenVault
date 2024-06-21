namespace TokenVault.Contracts.Cryptocurrency;

public record CreateCryptocurrencyRequest(
    string Symbol,
    string Name);

public record UpdateCryptocurrencyRequest(
    string? Symbol,
    string? Name);