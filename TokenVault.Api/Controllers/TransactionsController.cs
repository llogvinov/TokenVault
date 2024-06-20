using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.PortfolioAssets.Commands.CreatePortfolioAsset;
using TokenVault.Application.Features.PortfolioAssets.Commands.UpdatePortfolioAsset;
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
    private readonly IPortfolioAssetRepository _portfolioAssetRepository;
    private readonly ISender _mediatr;
    private readonly IMapper _mapper;

    public TransactionsController(
        ITransactionRepository transactionRepository,
        ISender mediatr,
        IMapper mapper,
        IPortfolioAssetRepository portfolioAssetRepository)
    {
        _transactionRepository = transactionRepository;
        _mediatr = mediatr;
        _mapper = mapper;
        _portfolioAssetRepository = portfolioAssetRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransactionAsync(
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

        var portfolioAsset = await _portfolioAssetRepository.GetPortfolioAssetAsync(
            transactionResult.CryptocurrencyId, transactionResult.PortfolioId);
        if (portfolioAsset is null)
        {
            var createPortfolioAssetCommand = new CreatePortfolioAssetCommand(
                transactionResult.CryptocurrencyId,
                transactionResult.PortfolioId,
                transactionResult.Amount,
                transactionResult.PricePerToken,
                transactionResult.TotalPrice);
            var portfolioAssetResult = await _mediatr.Send(createPortfolioAssetCommand);
        }
        else
        {
            var updatePortfolioAssetCommand = new UpdatePortfolioAssetCommand(
                transactionResult.CryptocurrencyId,
                transactionResult.PortfolioId,
                transactionResult);
            var portfolioAssetResult = await _mediatr.Send(updatePortfolioAssetCommand);
        }
        
        var response = _mapper.Map<CreateTransactionResponse>(transactionResult);
        
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetTransactionsByPortfolioIdAsync([FromRoute] Guid portfolioId)
    {
        var query = new GetTransactionsByPortfolioIdQuery(portfolioId);
        var transactionsResult = await _mediatr.Send(query);
        
        return Ok(transactionsResult.Transactions);
    }

    [HttpGet("{transactionId}")]
    public async Task<IActionResult> GetTransactionAsync([FromRoute] Guid transactionId)
    {
        var query = new GetTransactionByIdQuery(transactionId);
        var transaction = await _mediatr.Send(query);

        return Ok(transaction);
    }

    [HttpDelete("{transactionId}")]
    public async Task<IActionResult> DeleteTransactionAsync([FromRoute] Guid transactionId)
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

    [HttpGet("/transactions")]
    public async Task<IActionResult> GetTransactionsByUserIdAsync()
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
}