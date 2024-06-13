using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Transactions.Common;

namespace TokenVault.Application.Transactions.Commands.Delete;

public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, SingleTransactionResult>
{
    private readonly ITransactionRepository _transactionRepository;

    public DeleteTransactionCommandHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<SingleTransactionResult> Handle(DeleteTransactionCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var transactionResult = new SingleTransactionResult(
            command.Transaction.Id,
            command.Transaction.UserId,
            command.Transaction.AssetSymbol,
            command.Transaction.Quantity);
        _transactionRepository.Delete(command.Transaction.Id);

        return transactionResult;
    }
}