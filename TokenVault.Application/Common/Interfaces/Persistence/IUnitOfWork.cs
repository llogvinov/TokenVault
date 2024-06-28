namespace TokenVault.Application.Common.Interfaces.Persistence;

public interface IUnitOfWork
{
    ICryptocurrencyRepository Cryptocurrency { get; }
    IPortfolioRepository Portfolio { get; }
    
    void Save();
}