using TokenVault.Domain.Entities;

namespace TokenVault.Application.Common.Interfaces.Persistence;

public interface IPortfolioRepository
{
    void Add(Portfolio portfolio);

    void Delete(Guid id);

    Portfolio? GetPortfolioById(Guid id);
}