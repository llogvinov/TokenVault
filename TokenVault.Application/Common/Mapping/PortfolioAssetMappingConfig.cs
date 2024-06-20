using Mapster;
using TokenVault.Application.Features.PortfolioAssets.Commands.UpdatePortfolioAsset;
using TokenVault.Application.Features.PortfolioAssets.Common;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Common.Mapping;

public class PortfolioAssetMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UpdatePortfolioAssetCommand, PortfolioAsset>()
            .Map(dest => dest.CryptocurrencyId, src => src.CryptocurrencyId)
            .Map(dest => dest.PortfolioId, src => src.PortfolioId)
            .Map(dest => dest, src => src.UpdatePortfolioAssetDetails);
        
        config.NewConfig<PortfolioAsset, PortfolioAssetResult>();
    }
}