using MapsterMapper;
using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.Transactions.Common;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Features.Transactions.Commands.CreateTransaction;

public class CreateTransactionCommandHandler : 
    IRequestHandler<CreateTransactionCommand, SingleTransactionResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateTransactionCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SingleTransactionResult> Handle(
        CreateTransactionCommand command,
        CancellationToken cancellationToken)
    {
        var transaction = _mapper.Map<Transaction>(command);
        if (transaction.CreateDate == default)
        {
            transaction.CreateDate = DateTime.UtcNow;
        }
        await _unitOfWork.Transaction.AddAsync(transaction);

        var cryptocurrency = await _unitOfWork.Cryptocurrency.GetCryptocurrencyByIdAsync(command.CryptocurrencyId);
        var symbol = cryptocurrency?.Symbol ?? "Unknown";

        var transactionResult = _mapper.Map<SingleTransactionResult>((transaction, symbol));
        return transactionResult;
    }    
}