using MediatR;
using TokenVault.Application.Features.PortfolioAssets.Common;

namespace TokenVault.Application.Features.PortfolioAssets.Queries.GetPortfolioAssetsByPortfolioId;

public record GetPortfolioAssetsByPortfolioIdQuery(
    Guid PortfolioId) : IRequest<IEnumerable<PortfolioAssetResult>>;