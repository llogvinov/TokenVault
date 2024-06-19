using MediatR;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Features.Transactions.Queries.GetTransactionById;

public record GetTransactionByIdQuery(
    Guid transactionId) : IRequest<Transaction>;