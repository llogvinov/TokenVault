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
    public DbSet<Transaction> Transactions { get; set; } = null!;
    public DbSet<PortfolioAsset> PortfolioAssets { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PortfolioAsset>()
            .HasKey(pa => new { pa.PortfolioId, pa.CryptocurrencyId });

        modelBuilder.Entity<PortfolioAsset>()
            .HasOne(pa => pa.Portfolio)
            .WithMany(p => p.PortfolioAssets)
            .HasForeignKey(pa => pa.PortfolioId);

        modelBuilder.Entity<PortfolioAsset>()
            .HasOne(pa => pa.Cryptocurrency)
            .WithMany(c => c.PortfolioAssets)
            .HasForeignKey(pa => pa.CryptocurrencyId);
    }
}