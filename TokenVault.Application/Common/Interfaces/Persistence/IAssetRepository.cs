using TokenVault.Application.Assets;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Common.Interfaces.Persistence;

public interface IAssetRepository
{
    void Add(Asset asset);

    void Update(Guid portfolioId, Guid cryptocurrencyId, UpdateAssetDetails assetDetails);

    void Delete(Guid portfolioId, Guid cryptocurrencyId);

    void DeleteAllInPortfolio(Guid portfolioId);

    Asset? GetAssetInPortfolio(Guid portfolioId, Guid cryptocurrencyId);
}