using Mapster;
using TokenVault.Application.Transactions.Commands.Create;
using TokenVault.Contracts.Transactions;
using TokenVault.Domain.Entities;

namespace TokenVault.Api.Common.Mapping;

public class TransactionMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(CreateTransactionRequest request, Guid portfolioId), CreateTransactionCommand>()
            .Map(dest => dest.PortfolioId, src => src.portfolioId)
            .Map(dest => dest, src => src.request);

        config.NewConfig<Transaction, CreateTransactionResponse>();
    }
}