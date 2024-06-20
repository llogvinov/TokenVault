using MediatR;
using TokenVault.Application.Features.PortfolioAssets.Common;

namespace TokenVault.Application.Features.PortfolioAssets.Commands.UpdatePortfolioAsset;

public record UpdatePortfolioAssetCommand(
    Guid CryptocurrencyId,
    Guid PortfolioId,
    UpdatePortfolioAssetDetails UpdatePortfolioAssetDetails) : IRequest<PortfolioAssetResult>;