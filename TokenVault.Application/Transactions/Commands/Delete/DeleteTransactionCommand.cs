using MediatR;
using TokenVault.Application.Transactions.Common;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Transactions.Commands.Delete;

public record DeleteTransactionCommand(
    Transaction Transaction
) : IRequest<SingleTransactionResult>;