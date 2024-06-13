using Mapster;
using TokenVault.Application.Transactions.Commands.Create;
using TokenVault.Contracts.Transactions;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Common.Mapping;

public class TransactionMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(CreateTransactionRequest request, Guid userId, Guid portfolioId), CreateTransactionCommand>()
            .Map(dest => dest.UserId, src => src.userId)
            .Map(dest => dest.PortfolioId, src => src.portfolioId)
            .Map(dest => dest, src => src.request);

        config.NewConfig<Transaction, CreateTransactionResponse>();
    }
}