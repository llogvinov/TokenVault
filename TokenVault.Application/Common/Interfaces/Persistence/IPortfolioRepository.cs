using TokenVault.Domain.Entities;

namespace TokenVault.Application.Common.Interfaces.Persistence;

public interface IPortfolioRepository
{
    Task CreateAsync(Portfolio portfolio);

    Task DeleteAsync(Guid id);

    Task<IEnumerable<Portfolio>> GetPortfoliosAsync(Guid userId);

    Task<Portfolio?> GetPortfolioByIdAsync(Guid id);
}