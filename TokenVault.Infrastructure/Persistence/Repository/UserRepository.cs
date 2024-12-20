using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Domain.Entities;

namespace TokenVault.Infrastructure.Persistence.Repository;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(TokenVaultDbContext dbContext) 
        : base(dbContext) { }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        string query = "SELECT * FROM Users " +
            $"WHERE Email = '{email}'";
        return await QueryFirstOrDefaultAsync(query);
    }
}