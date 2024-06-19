using MediatR;
using TokenVault.Application.Features.Transactions.Common;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Features.Transactions.Commands.DeleteTransaction;

public record DeleteTransactionCommand(
    Transaction Transaction) : IRequest<SingleTransactionResult>;