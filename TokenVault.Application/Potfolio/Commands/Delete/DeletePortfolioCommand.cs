using MediatR;
using TokenVault.Application.Potfolio.Common;

namespace TokenVault.Application.Potfolio.Commands.Delete;

public record DeletePortfolioCommand(
    Guid PortfolioId
) : IRequest<PortfolioResult>;