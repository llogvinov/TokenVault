namespace TokenVault.Application.Potfolio.Common;

public record CreatePortfolioResult(
    Guid PortfolioId,
    Guid UserId,
    string Title
);