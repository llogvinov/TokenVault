using MediatR;
using TokenVault.Application.Features.PortfolioAssets.Common;
using TokenVault.Application.Features.Transactions.Common;

namespace TokenVault.Application.Features.PortfolioAssets.Commands.UpdatePortfolioAsset;

public record UpdatePortfolioAssetCommand(
    Guid CryptocurrencyId,
    Guid PortfolioId,
    SingleTransactionResult TransactionResult) : IRequest<PortfolioAssetResult>;