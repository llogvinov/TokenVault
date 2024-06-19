using MediatR;
using TokenVault.Application.Potfolio.Common;

namespace TokenVault.Application.Potfolio.Commands.Create;

public record CreatePortfolioCommand(
    string Title,
    Guid UserId
) : IRequest<PortfolioResult>;