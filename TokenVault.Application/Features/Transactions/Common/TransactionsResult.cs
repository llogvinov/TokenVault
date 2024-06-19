using TokenVault.Domain.Entities;

namespace TokenVault.Application.Features.Transactions.Common;

public record TransactionsResult(
    IEnumerable<Transaction> Transactions);