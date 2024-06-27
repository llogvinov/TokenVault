namespace TokenVault.Application.Common.Interfaces.Persistence;

public interface IUnitOfWork
{
    ICryptocurrencyRepository Cryptocurrency { get; }
    void Save();
}