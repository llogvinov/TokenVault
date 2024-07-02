using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Domain.Entities;

namespace TokenVault.Infrastructure.Persistence.Repository;

public class TransactionRepository : Repository<Transaction>, ITransactionRepository
{
    public TransactionRepository(TokenVaultDbContext dbContext)
        : base(dbContext) { }
}