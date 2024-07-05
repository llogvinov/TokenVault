using Mapster;
using TokenVault.Application.Features.Transactions.Commands.CreateTransaction;
using TokenVault.Application.Features.Transactions.Common;
using TokenVault.Contracts.Transactions;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Common.Mapping;

public class TransactionMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(CreateTransactionRequest request, Guid portfolioId), CreateTransactionCommand>()
            .Map(dest => dest.PortfolioId, src => src.portfolioId)
            .Map(dest => dest, src => src.request);

        config.NewConfig<CreateTransactionCommand, Transaction>()
            .Map(dest => dest.TransactionType, src => (TransactionType)src.TransactionType);
        
        config.NewConfig<(Transaction transaction, string Symbol), SingleTransactionResult>()
            .Map(dest => dest.AssetSymbol, src => src.Symbol)
            .Map(dest => dest.TransactionType, src => (int)src.transaction.TransactionType)
            .Map(dest => dest, src => src.transaction);

        config.NewConfig<SingleTransactionResult, CreateTransactionResponse>();
    }
}