using MediatR;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Features.PortfolioAssets.Queries.GetPortfolioAssetsByPortfolioId;

public record GetPortfolioAssetsByPortfolioIdQuery(
    Guid PortfolioId) : IRequest<IEnumerable<PortfolioAsset>>;