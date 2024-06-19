using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.Transactions.Commands.CreateTransaction;
using TokenVault.Application.Features.Transactions.Commands.DeleteTransaction;
using TokenVault.Application.Features.Transactions.Queries.GetTransactionById;
using TokenVault.Application.Features.Transactions.Queries.GetTransactionsByPortfolioId;
using TokenVault.Application.Features.Transactions.Queries.GetTransactionsByUserId;
using TokenVault.Contracts.Transactions;

namespace TokenVault.Api.Controllers;

[Route("portfolio/{portfolioId}/transactions")]
public class TransactionsController : ApiController
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly ISender _mediatr;
    private readonly IMapper _mapper;

    public TransactionsController(ITransactionRepository transactionRepository, ISender mediatr, IMapper mapper)
    {
        _transactionRepository = transactionRepository;
        _mediatr = mediatr;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransaction(
        [FromRoute] Guid portfolioId,
        [FromBody] CreateTransactionRequest request)
    {
        var userId = GetUserId();
        if (userId == default)
        {
            return Unauthorized();
        }

        var command = _mapper.Map<CreateTransactionCommand>((request, userId, portfolioId));
        var transactionResult = await _mediatr.Send(command);

        // _assetsService.UpdateAssetInPortfolio(transaction);
        
        var response = _mapper.Map<CreateTransactionResponse>(transactionResult);
        
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetTransactionsByPortfolioId([FromRoute] Guid portfolioId)
    {
        var query = new GetTransactionsByPortfolioIdQuery(portfolioId);
        var transactionsResult = await _mediatr.Send(query);
        
        return Ok(transactionsResult.Transactions);
    }

    [HttpGet("{transactionId}")]
    public async Task<IActionResult> GetTransaction([FromRoute] Guid transactionId)
    {
        var query = new GetTransactionByIdQuery(transactionId);
        var transaction = await _mediatr.Send(query);

        return Ok(transaction);
    }

    [HttpDelete("{transactionId}")]
    public async Task<IActionResult> DeleteTransaction([FromRoute] Guid transactionId)
    {
        var userId = GetUserId();
        if (userId == default)
        {
            return Unauthorized();
        }

        var query = new GetTransactionByIdQuery(transactionId);
        var transaction = await _mediatr.Send(query);

        if (userId != transaction.UserId)
        {
            throw new Exception("You have no rights to delete this transaction");
        }

        var command = new DeleteTransactionCommand(transaction);
        var transactionResult = await _mediatr.Send(command);

        // update transaction details

        return Ok(transactionResult);
    }

    [HttpGet]
    public async Task<IActionResult> GetTransactionsByUserId()
    {
        var userId = GetUserId();
        if (userId == default)
        {
            return Unauthorized();
        }

        var query = new GetTransactionsByUserIdQuery(userId);
        var transactionsResult = await _mediatr.Send(query);

        return Ok(transactionsResult.Transactions);
    }

    // [HttpPost("{portfolioId}/create")]
    // public async Task<IActionResult> CreateTransaction(
    //     [FromRoute] Guid portfolioId,
    //     [FromBody] CreateTransactionRequest request)
    // {
    //     var userId = GetUserId();
    //     if (userId == default)
    //     {
    //         return Unauthorized();
    //     }

    //     var response = await _transactionsService.CreateTransactionAsync(request, userId, portfolioId);

    //     return Ok(response);
    // }

    // [HttpGet]
    // public async Task<IActionResult> GetTransactionsByUserId()
    // {
    //     var userId = GetUserId();
    //     if (userId == default)
    //     {
    //         return Unauthorized();
    //     }

    //     var transactions = await _transactionsService.GetTransactionsByUserIdAsync(userId);

    //     return Ok(transactions);
    // }

    // [HttpGet("by-portfolio")]
    // public async Task<IActionResult> GetTransactionsByPortfolioId([FromQuery] Guid portfolioId)
    // {
    //     var transactions = await _transactionsService.GetTransactionsByPortfolioIdAsync(portfolioId);

    //     return Ok(transactions);
    // }

    // [HttpGet("by-cryptocurrency")]
    // public async Task<IActionResult> GetTransactionsByCryptocurrencyId([FromQuery] Guid cryptocurrencyId)
    // {
    //     var transactions = await _transactionsService.GetTransactionsByCryptocurrencyId(cryptocurrencyId);

    //     return Ok(transactions);
    // }

    // [HttpPost("{transactionId}/delete")]
    // public async Task<IActionResult> DeleteTransaction([FromRoute] Guid transactionId)
    // {
    //     var userId = GetUserId();
    //     if (userId == default)
    //     {
    //         return Unauthorized();
    //     }

    //     var response = await _transactionsService.DeleteTransactionAsync(userId, transactionId);

    //     return Ok(response);
    // }
}