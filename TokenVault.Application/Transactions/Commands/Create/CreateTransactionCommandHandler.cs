using MapsterMapper;
using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Transactions.Common;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Transactions.Commands.Create;

public partial class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, SingleTransactionResult>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly ICryptocurrencyRepository _cryptocurrencyRepository;
    private readonly IMapper _mapper;

    public CreateTransactionCommandHandler(
        ITransactionRepository transactionRepository,
        ICryptocurrencyRepository cryptocurrencyRepository,
        IMapper mapper)
    {
        _transactionRepository = transactionRepository;
        _cryptocurrencyRepository = cryptocurrencyRepository;
        _mapper = mapper;
    }

    public async Task<SingleTransactionResult> Handle(CreateTransactionCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var transactionDetails = GetTransactionDetails(command);
        var transaction = _mapper.Map<Transaction>((command, transactionDetails));
        _transactionRepository.Add(transaction);
        
        var cryptocurrency = _cryptocurrencyRepository.GetCryptocurrencyById(command.CryptocurrencyId);
        var symbol = cryptocurrency?.Symbol ?? "Unknown";

        var transactionResult = _mapper.Map<SingleTransactionResult>((transaction, symbol));

        return transactionResult;
    }

    private TransactionDetails GetTransactionDetails(CreateTransactionCommand command)
    {
        if (command.TotalPrice is null)
        {
            return CalculateTotal(command.PricePerToken, command.Amount);
        }
        else if (command.PricePerToken is null)
        {
            return CalculatePrice(command.Amount, command.TotalPrice);
        }
        else if (command.Amount is null)
        {
            return CalculateQuantity(command.PricePerToken, command.TotalPrice);
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