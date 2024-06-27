using Microsoft.EntityFrameworkCore;
using TokenVault.Domain.Entities;

namespace TokenVault.Infrastructure.Persistence;

public class TokenVaultDbContext : DbContext
{
    public TokenVaultDbContext(DbContextOptions<TokenVaultDbContext> options)
        : base(options) { }

    public DbSet<Cryptocurrency> Cryptocurrencies { get; set; } = null!;
}