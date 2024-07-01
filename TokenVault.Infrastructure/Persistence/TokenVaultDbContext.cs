using Microsoft.EntityFrameworkCore;
using TokenVault.Domain.Entities;

namespace TokenVault.Infrastructure.Persistence;

public class TokenVaultDbContext : DbContext
{
    public TokenVaultDbContext(DbContextOptions<TokenVaultDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Cryptocurrency> Cryptocurrencies { get; set; } = null!;
    public DbSet<Portfolio> Portfolios { get; set; } = null!;
}