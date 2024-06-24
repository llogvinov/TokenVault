using MediatR;
using TokenVault.Application.Features.Portfolios.Common;

namespace TokenVault.Application.Features.Portfolios.Queries.GetPortfolioById;

public record GetPortfolioByIdQuery(
    Guid PortfolioId) : IRequest<PortfolioResult>;