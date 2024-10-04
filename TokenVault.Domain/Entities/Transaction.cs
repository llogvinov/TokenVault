namespace TokenVault.Domain.Entities;

public class Transaction
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid PortfolioId { get; set; }
    public Guid CryptocurrencyId { get; set; }
    public TransactionType TransactionType { get; set; }
    public double Amount { get; set; }
    public double PricePerToken { get; set; }
    public double TotalPrice { get; set; }
    public DateTime CreateDate { get; set; }
}

public enum TransactionType
{
    Buy = 1,
    Sell = 2,
    Deposit = 3,
}