using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Domain.Entities;

namespace TokenVault.Infrastructure.Persistence.Repository;

public class PortfolioRepository : Repository<Portfolio>, IPortfolioRepository
{
    public PortfolioRepository(TokenVaultDbContext dbContext) 
        : base(dbContext) { }

    public async Task<Portfolio> UpdateAsync(Guid id, string title)
    {
        var portfolioFromDb = await GetFirstOrDefaultAsync(p => p.Id == id);
        if (portfolioFromDb is null)
        {
            throw new ArgumentNullException(nameof(portfolioFromDb),
                $"Portfolio with given id: {id} does not exist");
        }

        portfolioFromDb.Title = title ?? portfolioFromDb.Title;
        
        return portfolioFromDb;
    }
}