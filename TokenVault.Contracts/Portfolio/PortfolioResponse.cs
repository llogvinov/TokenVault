namespace TokenVault.Contracts.Portfolio;

public record PortfolioResponse(
    Guid Id,
    Guid UserId,
    string Title
);