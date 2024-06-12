using MediatR;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Potfolio.Commands.Create;

public record CreatePortfolioCommand(
    string Title,
    Guid UserId
) : IRequest<Portfolio>;