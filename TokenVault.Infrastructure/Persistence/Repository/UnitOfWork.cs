using TokenVault.Application.Common.Interfaces.Persistence;

namespace TokenVault.Infrastructure.Persistence.Repository;

public class UnitOfWork : IUnitOfWork
{
    private TokenVaultDbContext _dbContext;

    public UnitOfWork(TokenVaultDbContext dbContext)
    {
        _dbContext = dbContext;
        Cryptocurrency = new CryptocurrencyRepository(_dbContext);
        Portfolio = new PortfolioRepository(_dbContext);
    }

    public ICryptocurrencyRepository Cryptocurrency { get; private set; }
    public IPortfolioRepository Portfolio { get; private set; }

    public void Save()
    {
        _dbContext.SaveChanges();
    }
}