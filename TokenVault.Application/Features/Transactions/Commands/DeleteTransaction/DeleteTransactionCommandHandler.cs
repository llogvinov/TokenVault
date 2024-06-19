using MapsterMapper;
using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.Transactions.Common;

namespace TokenVault.Application.Features.Transactions.Commands.DeleteTransaction;

public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, SingleTransactionResult>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;

    public DeleteTransactionCommandHandler(
        ITransactionRepository transactionRepository,
        IMapper mapper)
    {
        _transactionRepository = transactionRepository;
        _mapper = mapper;
    }

    public async Task<SingleTransactionResult> Handle(
        DeleteTransactionCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var transactionResult = _mapper.Map<SingleTransactionResult>(command.Transaction);
        _transactionRepository.Delete(command.Transaction.Id);

        return transactionResult;
    }
}