namespace TokenVault.Application.Potfolio.Common;

public record PortfolioResult(
    Guid PortfolioId,
    Guid UserId,
    string Title
);