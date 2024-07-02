using MapsterMapper;
using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.Transactions.Common;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Features.Transactions.Commands.CreateTransaction;

public class CreateTransactionCommandHandler : 
    IRequestHandler<CreateTransactionCommand, SingleTransactionResult>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateTransactionCommandHandler(
        ITransactionRepository transactionRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _transactionRepository = transactionRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<SingleTransactionResult> Handle(
        CreateTransactionCommand command,
        CancellationToken cancellationToken)
    {
        var transaction = _mapper.Map<Transaction>(command);
        await _transactionRepository.CreateAsync(transaction);

        var cryptocurrency = await _unitOfWork.Cryptocurrency.GetFirstOrDefaultAsync(
            c => c.Id == command.CryptocurrencyId);
        var symbol = cryptocurrency?.Symbol ?? "Unknown";

        var transactionResult = _mapper.Map<SingleTransactionResult>((transaction, symbol));
        return transactionResult;
    }    
}