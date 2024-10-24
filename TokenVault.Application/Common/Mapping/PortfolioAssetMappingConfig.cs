using Mapster;
using TokenVault.Application.Features.PortfolioAssets.Commands.UpdatePortfolioAsset;
using TokenVault.Application.Features.PortfolioAssets.Common;
using TokenVault.Application.Features.Transactions.Common;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Common.Mapping;

public class PortfolioAssetMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UpdatePortfolioAssetCommand, PortfolioAsset>()
            .Map(dest => dest.CryptocurrencyId, src => src.CryptocurrencyId)
            .Map(dest => dest.PortfolioId, src => src.PortfolioId)
            .Map(dest => dest.Holdings, src => src.UpdatePortfolioAssetDetails.Amount)
            .Map(dest => dest.AveragePrice, src => src.UpdatePortfolioAssetDetails.PricePerToken)
            .Map(dest => dest.Invested, src => src.UpdatePortfolioAssetDetails.TotalPrice);
        
        config.NewConfig<(PortfolioAsset portfolioAsset, double price, double profit), PortfolioAssetResult>()
            .Map(dest => dest.TokenPrice, src => src.price)
            .Map(dest => dest.CurrentProfit, src => src.profit)
            .Map(dest => dest, src => src.portfolioAsset)
        ;

        config.NewConfig<SingleTransactionResult, UpdatePortfolioAssetDetails>();

        config.NewConfig<(SingleTransactionResult result, UpdatePortfolioAssetDetails details), UpdatePortfolioAssetCommand>()
            .Map(dest => dest.CryptocurrencyId, src => src.result.CryptocurrencyId)
            .Map(dest => dest.PortfolioId, src => src.result.PortfolioId)
            .Map(dest => dest.UpdatePortfolioAssetDetails, src => src.details);
    }
}