using MapsterMapper;
using MediatR;
using TokenVault.Application.Assets;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.Transactions.Commands.CreateTransaction;
using TokenVault.Application.Features.Transactions.Commands.DeleteTransaction;
using TokenVault.Application.Features.Transactions.Common;
using TokenVault.Application.Features.Transactions.Queries.GetTransactionsByPortfolioId;
using TokenVault.Application.Features.Transactions.Queries.GetTransactionsByUserId;
using TokenVault.Application.Transactions.Queries.GetByCryptocurrencyId;
using TokenVault.Contracts.Transactions;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Transactions;

public class TransactionsService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly AssetsService _assetsService;
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public TransactionsService(
        ITransactionRepository transactionRepository,
        AssetsService assetsService,
        ISender mediator,
        IMapper mapper)
    {
        _transactionRepository = transactionRepository;
        _assetsService = assetsService;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<CreateTransactionResponse> CreateTransactionAsync(
        CreateTransactionRequest request,
        Guid userId,
        Guid portfolioId)
    {
        var command = _mapper.Map<CreateTransactionCommand>((request, userId, portfolioId));
        var transaction = await _mediator.Send(command);

        _assetsService.UpdateAssetInPortfolio(transaction);
        
        var response = _mapper.Map<CreateTransactionResponse>(transaction);

        return response;
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByUserIdAsync(Guid userId)
    {
        var query = new GetTransactionsByUserIdQuery(userId);
        var transactionsResult = await _mediator.Send(query);

        return transactionsResult.Transactions;
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByPortfolioIdAsync(Guid portfolioId)
    {
        var query = new GetTransactionsByPortfolioIdQuery(portfolioId);
        var transactionsResult = await _mediator.Send(query);

        return transactionsResult.Transactions;
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByCryptocurrencyId(Guid cryptocurrencyId)
    {
        var query = new GetTransactionsByCryptocurrencyIdQuery(cryptocurrencyId);
        var transactionsResult = await _mediator.Send(query);

        return transactionsResult.Transactions;
    }

    public async Task<SingleTransactionResult> DeleteTransactionAsync(Guid userId, Guid transactionId)
    {
        var transaction = _transactionRepository.GetTransactionById(transactionId);
        if (transaction is null)
        {
            throw new ArgumentNullException(nameof(transaction));
        }
        else if (userId != transaction.UserId)
        {
            throw new Exception("You have no rights to delete this transaction");
        }

        var command = new DeleteTransactionCommand(transaction);
        var transactionResult = await _mediator.Send(command);

        // update transaction details

        return transactionResult;
    }

    public void DeleteAllPortfolioTransactions(Guid portfolioId)
    {
        _transactionRepository.DeleteByPortfolioId(portfolioId);
    }
}