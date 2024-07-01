using TokenVault.Application.Common.Interfaces.Persistence;

namespace TokenVault.Infrastructure.Persistence.Repository;

public class UnitOfWork : IUnitOfWork
{
    private TokenVaultDbContext _dbContext;

    public UnitOfWork(TokenVaultDbContext dbContext)
    {
        _dbContext = dbContext;
        User = new UserRepository(_dbContext);
        Cryptocurrency = new CryptocurrencyRepository(_dbContext);
        Portfolio = new PortfolioRepository(_dbContext);
    }

    public IUserRepository User { get; private set; }
    public ICryptocurrencyRepository Cryptocurrency { get; private set; }
    public IPortfolioRepository Portfolio { get; private set; }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}