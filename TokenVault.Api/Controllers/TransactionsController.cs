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
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetTransactionsByPortfolioId([FromRoute] Guid portfolioId)
    {
        var query = new GetTransactionsByPortfolioIdQuery(portfolioId);
        var transactionsResult = await _mediatr.Send(query);
        return Ok(transactionsResult.Transactions);
    }

    /// <summary>
    ///     Get transaction by id
    /// </summary>
    /// <param name="transactionId">Id of transaction</param>
    /// <returns></returns>
    [HttpGet("{transactionId}")]
    public async Task<IActionResult> GetTransaction([FromRoute] Guid transactionId)
    {
        var query = new GetTransactionByIdQuery(transactionId);
        var transaction = await _mediatr.Send(query);
        return Ok(transaction);
    }

    /// <summary>
    ///     Create transaction
    /// </summary>
    /// <param name="portfolioId">Id of portfolio, where transaction is created</param>
    /// <param name="request">Create transaction request</param>
    /// <returns></returns>
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

        var command = _mapper.Map<CreateTransactionCommand>((request, portfolioId));
        var transactionResult = await _mediatr.Send(command);

        var updatePortfolioAssetDetails = _mapper.Map<UpdatePortfolioAssetDetails>(transactionResult);
        var updatePortfolioAssetCommand = _mapper.Map<UpdatePortfolioAssetCommand>(
            (transactionResult, updatePortfolioAssetDetails));
        var portfolioAssetResult = await _mediatr.Send(updatePortfolioAssetCommand);
        
        await _unitOfWork.SaveAsync();

        var response = _mapper.Map<CreateTransactionResponse>(transactionResult);
        return Ok(response);
    }

    /// <summary>
    ///     Delete transaction
    /// </summary>
    /// <param name="transactionId">Id of transaction</param>
    /// <returns></returns>
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

        var portfolio = await _unitOfWork.Portfolio.GetFirstOrDefaultAsync(
            p => p.Id == transaction.PortfolioId);
        var transactionUserId = portfolio.UserId;
        if (userId != transactionUserId)
        {
            throw new Exception("You have no rights to delete this transaction");
        }

        var command = new DeleteTransactionCommand(transaction);
        var transactionResult = await _mediatr.Send(command);
        await _unitOfWork.SaveAsync();
        return Ok(transactionResult);
    }

    [HttpGet("/transactions")]
    public async Task<IActionResult> GetTransactionsByUserId()
    {
        // TODO
        return Ok();
    }
}