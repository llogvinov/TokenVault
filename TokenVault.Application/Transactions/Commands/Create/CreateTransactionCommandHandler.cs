using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Transactions.Commands.Create;

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, Transaction>
{
    public ITransactionRepository _transactionRepository;

    public CreateTransactionCommandHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<Transaction> Handle(CreateTransactionCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var transactionDetails = GetTransactionDetails(command);

        var transaction = new Transaction
        {
            AssetSymbol = command.AssetSymbol,
            PortfolioId = command.PortfolioId,
            Quantity = transactionDetails.Quantity,
            Price = transactionDetails.Price,
            Total = transactionDetails.Total,
            Date = DateTime.UtcNow,
        };
        _transactionRepository.Add(transaction);

        return transaction;
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


    public record TransactionDetails(
        double Quantity,
        double Price,
        double Total
    );
}