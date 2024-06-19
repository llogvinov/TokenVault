using MediatR;
using TokenVault.Application.Features.Potfolios.Common;

namespace TokenVault.Application.Features.Potfolios.Commands.CreatePortfolio;

public record CreatePortfolioCommand(
    Guid UserId,
    string Title
) : IRequest<PortfolioResult>;