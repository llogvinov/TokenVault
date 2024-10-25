using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Domain.Entities;

namespace TokenVault.Infrastructure.Persistence.Repository;

public class PortfolioRepository : Repository<Portfolio>, IPortfolioRepository
{
    public PortfolioRepository(TokenVaultDbContext dbContext) 
        : base(dbContext) { }

    public async Task<Portfolio> UpdateAsync(Guid id, string title)
    {
        var portfolioFromDb = await GetPortfolioByIdAsync(id);
        if (portfolioFromDb is null)
        {
            throw new ArgumentNullException(nameof(portfolioFromDb),
                $"Portfolio with given id: {id} does not exist");
        }

        portfolioFromDb.Title = title ?? portfolioFromDb.Title;
        return portfolioFromDb;
    }

    public async Task<List<Portfolio>> GetPortfoliosAsync()
    {
        var query = "SELECT * FROM Portfolios";
        return await QueryAsync(query);
    }

    public async Task<List<Portfolio>> GetPortfoliosByUserIdAsync(Guid userId)
    {
        var query = $"SELECT * FROM Portfolios WHERE UserId = '{userId}'";
        return await QueryAsync(query);
    }

    public async Task<Portfolio?> GetPortfolioByIdAsync(Guid id)
    {
        var query = $"SELECT * FROM Portfolios WHERE Id = '{id}'";
        return await QueryFirstOrDefaultAsync(query);
    }
}