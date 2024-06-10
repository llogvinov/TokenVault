using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Domain.Entities;

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, Transaction>
{
    public ITransactionRepository _transactionRepository;

    public CreateTransactionCommandHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<Transaction> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var transactionDetails = GetTransactionDetails(request);

        var transaction = new Transaction
        {
            AssetSymbol = request.AssetSymbol,
            Quantity = transactionDetails.Quantity,
            Price = transactionDetails.Price,
            Total = transactionDetails.Total,
            Date = DateTime.UtcNow,
        };
        _transactionRepository.Add(transaction);

        return transaction;
    }

    private TransactionDetails GetTransactionDetails(CreateTransactionCommand request)
    {
        if (request.Total is null) 
        {
            var total = CalculateTotal(request.Price, request.Quantity);
            return new TransactionDetails(total, (double)request.Price, (double)request.Quantity);
        }
        else if (request.Price is null) 
        {
            var price = CalculatePrice(request.Quantity, request.Total);
            return new TransactionDetails((double)request.Total, price, (double)request.Quantity);
        }
        else if (request.Quantity is null)
        {
            var quantity = CalculateQuantity(request.Price, request.Total);
            return new TransactionDetails((double)request.Total, (double)request.Price, quantity);
        }
        
        throw new ArgumentException("Exactly one of the parameters must be null and the others must be non-null.");
    }

    private double CalculateTotal(double? price, double? quantity)
    {
        if (price is double p && 
            quantity is double q)
        {
            return p * q;
        }

        throw new ArgumentException("price or quantity is null");
    }
    
    private double CalculateQuantity(double? price, double? total)
    {
        if (price is double p && 
            total is double t)
        {
            return t / p;
        }

        throw new ArgumentException("price or total is null");
    }
    
    private double CalculatePrice(double? quantity, double? total)
    {
        if (quantity is double q && 
            total is double t)
        {
            return t / q;
        }

        throw new ArgumentException("quantity or total is null");
    }


    public record TransactionDetails(
        double Quantity,
        double Price,
        double Total
    );
}