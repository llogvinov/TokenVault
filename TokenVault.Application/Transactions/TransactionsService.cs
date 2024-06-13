using MapsterMapper;
using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Transactions.Commands.Create;
using TokenVault.Application.Transactions.Queries.GetByPortfolioId;
using TokenVault.Application.Transactions.Queries.GetByUserId;
using TokenVault.Contracts.Transactions;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Transactions;

public class TransactionsService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public TransactionsService(ITransactionRepository transactionRepository, ISender mediator, IMapper mapper)
    {
        _transactionRepository = transactionRepository;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<CreateTransactionResponse> CreateTransaction(
        CreateTransactionRequest request,
        Guid userId,
        Guid portfolioId)
    {
        var command = _mapper.Map<CreateTransactionCommand>((request, userId, portfolioId));
        var transaction = await _mediator.Send(command);
        
        var response = _mapper.Map<CreateTransactionResponse>(transaction);

        return response;
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByUserId(Guid userId)
    {
        var query = new GetTransactionsByUserIdQuery(userId);
        var transactionsResult = await _mediator.Send(query);

        return transactionsResult.Transactions;
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByPortfolioId(Guid portfolioId)
    {
        var query = new GetTransactionsByPortfolioIdQuery(portfolioId);
        var transactionsResult = await _mediator.Send(query);

        return transactionsResult.Transactions;
    }
}