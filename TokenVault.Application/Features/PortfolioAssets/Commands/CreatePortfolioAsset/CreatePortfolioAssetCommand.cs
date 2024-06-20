using MediatR;
using TokenVault.Application.Features.PortfolioAssets.Common;

namespace TokenVault.Application.Features.PortfolioAssets.Commands.CreatePortfolioAsset;

public record CreatePortfolioAssetCommand(
    Guid CryptocurrencyId,
    Guid PortfolioId,
    double Amount,
    double AveragePrice,
    double Invested) : IRequest<PortfolioAssetResult>;