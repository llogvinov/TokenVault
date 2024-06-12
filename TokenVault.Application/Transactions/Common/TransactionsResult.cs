using TokenVault.Domain.Entities;

namespace TokenVault.Application.Transactions.Common;

public record TransactionsResult(
    IEnumerable<Transaction> Transactions
);