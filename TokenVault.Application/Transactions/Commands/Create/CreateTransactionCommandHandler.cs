using MapsterMapper;
using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Transactions.Common;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Transactions.Commands.Create;

public partial class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, SingleTransactionResult>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;

    public CreateTransactionCommandHandler(ITransactionRepository transactionRepository, IMapper mapper)
    {
        _transactionRepository = transactionRepository;
        _mapper = mapper;
    }

    public async Task<SingleTransactionResult> Handle(CreateTransactionCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var transactionDetails = GetTransactionDetails(command);
        var transaction = _mapper.Map<Transaction>((command, transactionDetails));
        _transactionRepository.Add(transaction);

        var transactionResult = _mapper.Map<SingleTransactionResult>(transaction);

        return transactionResult;
    }

    private TransactionDetails GetTransactionDetails(CreateTransactionCommand command)
    {
        if (command.Total is null)
        {
            return CalculateTotal(command.Price, command.Quantity);
        }
        else if (command.Price is null)
        {
            return CalculatePrice(command.Quantity, command.Total);
        }
        else if (command.Quantity is null)
        {
            return CalculateQuantity(command.Price, command.Total);
        }

        throw new ArgumentException("Exactly one of the parameters must be null and the others must be non-null.");
    }

    private TransactionDetails CalculateTotal(double? price, double? quantity)
    {
        if (price is double p &&
            quantity is double q)
        {
            var t = p * q;
            return new TransactionDetails(q, p, t);
        }

        throw new ArgumentException("price or quantity is null");
    }

    private TransactionDetails CalculateQuantity(double? price, double? total)
    {
        if (price is double p &&
            total is double t)
        {
            var q = t / p;
            return new TransactionDetails(q, p, t);
        }

        throw new ArgumentException("price or total is null");
    }

    private TransactionDetails CalculatePrice(double? quantity, double? total)
    {
        if (quantity is double q &&
            total is double t)
        {
            var p = t / q;
            return new TransactionDetails(q, p, t);
        }

        throw new ArgumentException("quantity or total is null");
    }
}