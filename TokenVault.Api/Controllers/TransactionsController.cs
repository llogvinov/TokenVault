using Microsoft.AspNetCore.Mvc;
using TokenVault.Application.Transactions;
using TokenVault.Contracts.Transactions;

namespace TokenVault.Api.Controllers;

public class TransactionsController : ApiController
{
    private TransactionsService _transactionsService;

    public TransactionsController(TransactionsService transactionsService)
    {
        _transactionsService = transactionsService;
    }

    /// <summary>
    /// create transaction
    /// </summary>
    [HttpPost("{portfolioId}/create")]
    public async Task<IActionResult> CreateTransaction(
        [FromRoute] Guid portfolioId,
        [FromBody] CreateTransactionRequest request)
    {
        var userId = GetUserId();
        if (userId == default)
        {
            return Unauthorized();
        }

        var response = await _transactionsService.CreateTransactionAsync(request, userId, portfolioId);

        return Ok(response);
    }

    /// <summary>
    /// get transactions by user id
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetTransactionsByUserId()
    {
        var userId = GetUserId();
        if (userId == default)
        {
            return Unauthorized();
        }

        var transactions = await _transactionsService.GetTransactionsByUserIdAsync(userId);

        return Ok(transactions);
    }

    /// <summary>
    /// get transactions by portfolio id
    /// </summary>
    [HttpGet("by-portfolio")]
    public async Task<IActionResult> GetTransactionsByPortfolioId([FromQuery] Guid portfolioId)
    {
        var transactions = await _transactionsService.GetTransactionsByPortfolioIdAsync(portfolioId);

        return Ok(transactions);
    }

    /// <summary>
    /// get transactions by portfolio id
    /// </summary>
    [HttpGet("by-cryptocurrency")]
    public async Task<IActionResult> GetTransactionsByCryptocurrencyId([FromQuery] Guid cryptocurrencyId)
    {
        var transactions = await _transactionsService.GetTransactionsByCryptocurrencyId(cryptocurrencyId);

        return Ok(transactions);
    }

    /// <summary>
    /// delete transaction by id
    /// </summary>
    [HttpPost("{transactionId}/delete")]
    public async Task<IActionResult> DeleteTransaction([FromRoute] Guid transactionId)
    {
        var userId = GetUserId();
        if (userId == default)
        {
            return Unauthorized();
        }

        var response = await _transactionsService.DeleteTransactionAsync(userId, transactionId);

        return Ok(response);
    }
}