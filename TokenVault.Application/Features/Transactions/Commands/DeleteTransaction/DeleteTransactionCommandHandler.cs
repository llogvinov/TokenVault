using MapsterMapper;
using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.Transactions.Common;

namespace TokenVault.Application.Features.Transactions.Commands.DeleteTransaction;

public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, SingleTransactionResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeleteTransactionCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SingleTransactionResult> Handle(
        DeleteTransactionCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
        var transactionResult = _mapper.Map<SingleTransactionResult>(command.Transaction);
        _unitOfWork.Transaction.Remove(command.Transaction);

        return transactionResult;
    }
}