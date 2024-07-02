namespace TokenVault.Application.Common.Interfaces.Persistence;

public interface IUnitOfWork
{
    IUserRepository User { get; }
    ICryptocurrencyRepository Cryptocurrency { get; }
    IPortfolioRepository Portfolio { get; }
    ITransactionRepository Transaction { get; }
    
    Task SaveAsync();
}