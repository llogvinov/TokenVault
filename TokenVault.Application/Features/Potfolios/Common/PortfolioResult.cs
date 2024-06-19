namespace TokenVault.Application.Features.Potfolios.Common;

public record PortfolioResult(
    Guid Id,
    Guid UserId,
    string Title
);