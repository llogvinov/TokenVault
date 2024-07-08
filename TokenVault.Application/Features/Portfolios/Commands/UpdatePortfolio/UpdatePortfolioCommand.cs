using MediatR;
using TokenVault.Application.Features.Portfolios.Common;

namespace TokenVault.Application.Features.Portfolios.Commands.UpdatePortfolio;

public record UpdatePortfolioCommand(
    Guid Id,
    string Title) : IRequest<PortfolioResult>;