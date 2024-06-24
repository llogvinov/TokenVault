namespace TokenVault.Application.Features.Portfolios.Common;

public record PortfolioResult(
    Guid Id,
    Guid UserId,
    string Title);