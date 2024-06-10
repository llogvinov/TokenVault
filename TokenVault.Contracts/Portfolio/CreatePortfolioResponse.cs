namespace TokenVault.Contracts.Portfolio;

public record CreatePortfolioResponse(
    Guid Id,
    Guid UserId,
    string Title
);