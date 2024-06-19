using MediatR;
using TokenVault.Application.Features.Potfolios.Common;

namespace TokenVault.Application.Features.Potfolios.Queries.GetPortfolioById;

public record GetPortfolioByIdQuery(
    Guid PortfolioId) : IRequest<PortfolioResult>;