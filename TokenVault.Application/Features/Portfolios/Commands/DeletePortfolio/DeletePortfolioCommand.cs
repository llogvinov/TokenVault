using MediatR;
using TokenVault.Application.Features.Portfolios.Common;

namespace TokenVault.Application.Features.Portfolios.Commands.DeletePortfolio;

public record DeletePortfolioCommand(
    Guid PortfolioId) : IRequest<PortfolioResult>;