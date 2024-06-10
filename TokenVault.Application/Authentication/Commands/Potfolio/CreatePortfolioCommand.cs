using MediatR;
using TokenVault.Domain.Entities;

public record CreatePortfolioCommand(
    string Title,
    Guid UserId
) : IRequest<Portfolio>;