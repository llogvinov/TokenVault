using Mapster;
using TokenVault.Application.Transactions.Commands.Create;
using TokenVault.Application.Transactions.Common;
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

        config.NewConfig<(CreateTransactionCommand command, TransactionDetails details), Transaction>()
            .Map(dest => dest.Amount, src => src.details.Amount)
            .Map(dest => dest.PricePerToken, src => src.details.PricePerToken)
            .Map(dest => dest.TotalPrice, src => src.details.TotalPrice)
            .Map(dest => dest, src => src.command);
        
        config.NewConfig<(Transaction transaction, string Symbol), SingleTransactionResult>()
            .Map(dest => dest.AssetSymbol, src => src.Symbol)
            .Map(dest => dest, src => src.transaction);

        config.NewConfig<SingleTransactionResult, CreateTransactionResponse>();
    }
}