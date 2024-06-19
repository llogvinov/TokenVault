using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Domain.Entities;

public class PortfolioRepository : IPortfolioRepository
{
    private static readonly List<Portfolio> _portfolios = new();

    public async Task CreateAsync(Portfolio portfolio)
    {
        await Task.CompletedTask;

        _portfolios.Add(portfolio);
    }

    public async Task DeleteAsync(Guid id)
    {
        await Task.CompletedTask;
        
        var portfolio = _portfolios.FirstOrDefault(p => p.Id == id);
        if (portfolio is not null)
        {
            _portfolios.Remove(portfolio);
        }
    }

    public async Task<IEnumerable<Portfolio>> GetPortfoliosAsync(Guid userId)
    {
        await Task.CompletedTask;

        return _portfolios.Where(p => p.UserId == userId);
    }

    public async Task<Portfolio?> GetPortfolioByIdAsync(Guid id)
    {
        await Task.CompletedTask;

        return _portfolios.SingleOrDefault(p => p.Id == id);
    }
}