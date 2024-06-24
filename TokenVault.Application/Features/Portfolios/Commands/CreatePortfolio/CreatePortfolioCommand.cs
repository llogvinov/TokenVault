using MediatR;
using TokenVault.Application.Features.Portfolios.Common;

namespace TokenVault.Application.Features.Portfolios.Commands.CreatePortfolio;

public record CreatePortfolioCommand(
    Guid UserId,
    string Title) : IRequest<PortfolioResult>;