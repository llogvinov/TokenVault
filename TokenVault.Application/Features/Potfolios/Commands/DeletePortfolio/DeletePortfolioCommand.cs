using MediatR;
using TokenVault.Application.Features.Potfolios.Common;

namespace TokenVault.Application.Features.Potfolios.Commands.DeletePortfolio;

public record DeletePortfolioCommand(
    Guid PortfolioId
) : IRequest<PortfolioResult>;