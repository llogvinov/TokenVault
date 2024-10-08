using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.PortfolioAssets.Commands.UpdatePortfolioAsset;
using TokenVault.Application.Features.PortfolioAssets.Common;
using TokenVault.Application.Features.Transactions.Commands.CreateTransaction;
using TokenVault.Application.Features.Transactions.Commands.DeleteTransaction;
using TokenVault.Application.Features.Transactions.Queries.GetTransactionById;
using TokenVault.Application.Features.Transactions.Queries.GetTransactionsByPortfolioId;
using TokenVault.Contracts.Transactions;

namespace TokenVault.Api.Controllers;

[Route("portfolio/{portfolioId}/transactions")]
public class TransactionsController : ApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISender _mediatr;
    private readonly IMapper _mapper;

    public TransactionsController(
        IUnitOfWork unitOfWork,
        ISender mediatr,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mediatr = mediatr;
        _mapper = mapper;
    }

    /// <summary>
    ///     Get transactions in certain portfolio
    /// </summary>
    /// <param name="portfolioId">Id of portfolio, where transaction was added</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetTransactionsByPortfolioIdAsync(
        [FromRoute] Guid portfolioId,
        CancellationToken cancellationToken = default)
    {
        var query = new GetTransactionsByPortfolioIdQuery(portfolioId);
        var transactionsResult = await _mediatr.Send(query, cancellationToken);
        return Ok(transactionsResult.Transactions);
    }

    /// <summary>
    ///     Get transaction by id
    /// </summary>
    /// <param name="transactionId">Id of transaction</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{transactionId}")]
    public async Task<IActionResult> GetTransactionAsync(
        [FromRoute] Guid transactionId,
        CancellationToken cancellationToken = default)
    {
        var query = new GetTransactionByIdQuery(transactionId);
        var transaction = await _mediatr.Send(query, cancellationToken);
        return Ok(transaction);
    }

    /// <summary>
    ///     Create transaction
    /// </summary>
    /// <param name="portfolioId">Id of portfolio, where transaction is created</param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateTransactionAsync(
        [FromRoute] Guid portfolioId,
        [FromBody] CreateTransactionRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<CreateTransactionCommand>((request, portfolioId));
        var transactionResult = await _mediatr.Send(command, cancellationToken);

        var updatePortfolioAssetDetails = _mapper.Map<UpdatePortfolioAssetDetails>(transactionResult);
        var updatePortfolioAssetCommand = _mapper.Map<UpdatePortfolioAssetCommand>(
            (transactionResult, updatePortfolioAssetDetails));
        var portfolioAssetResult = await _mediatr.Send(updatePortfolioAssetCommand, cancellationToken);

        await _unitOfWork.SaveAsync();

        var response = _mapper.Map<CreateTransactionResponse>(transactionResult);
        return CreatedAtAction(nameof(GetTransactionAsync),
            new { portfolioId = response.PortfolioId, transactionId = response.Id },
            request);
    }

    /// <summary>
    ///     Delete transaction
    /// </summary>
    /// <param name="transactionId">Id of transaction</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{transactionId}")]
    public async Task<IActionResult> DeleteTransaction(
        [FromRoute] Guid transactionId,
        CancellationToken cancellationToken = default)
    {
        var userId = GetUserId();
        var query = new GetTransactionByIdQuery(transactionId);
        var transaction = await _mediatr.Send(query, cancellationToken);

        var portfolio = await _unitOfWork.Portfolio.GetPortfolioByIdAsync(transaction.PortfolioId);
        var transactionUserId = portfolio?.UserId;
        if (userId != transactionUserId)
        {
            throw new Exception("You have no rights to delete this transaction");
        }

        var command = new DeleteTransactionCommand(transaction);
        var transactionResult = await _mediatr.Send(command, cancellationToken);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

    [HttpGet("/transactions")]
    public IActionResult GetTransactionsByUserId()
    {
        // TODO
        var userId = GetUserId();
        return Ok();
    }
}