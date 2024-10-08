using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.Transactions.Common;

namespace TokenVault.Application.Features.Transactions.Queries.GetTransactionsByPortfolioId;

public class GetTransactionsByPortfolioIdQueryHandler : 
    IRequestHandler<GetTransactionsByPortfolioIdQuery, TransactionsResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTransactionsByPortfolioIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TransactionsResult> Handle(
        GetTransactionsByPortfolioIdQuery query,
        CancellationToken cancellationToken)
    {
        var transactions = await _unitOfWork.Transaction.GetTransactionsByPortfolioIdAsync(query.PortfolioId);

        var transactionsResult = new TransactionsResult(transactions);
        return transactionsResult;
    }
}