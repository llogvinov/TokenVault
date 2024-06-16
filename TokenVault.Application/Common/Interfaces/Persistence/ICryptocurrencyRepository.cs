using TokenVault.Domain.Entities;

namespace TokenVault.Application.Common.Interfaces.Persistence;

public interface ICryptocurrencyRepository
{
    void Add(Cryptocurrency cryptocurrency);

    void Delete(Guid cryptocurrencyId);
    
    IEnumerable<Cryptocurrency> GetAll();
}