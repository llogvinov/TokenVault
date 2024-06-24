using Mapster;
using TokenVault.Application.Features.Portfolios.Commands.CreatePortfolio;
using TokenVault.Application.Features.Portfolios.Common;
using TokenVault.Contracts.Portfolio;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Common.Mapping;

public class PortfolioMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(Guid userId, CreatePortfolioRequest request), CreatePortfolioCommand>()
            .Map(dest => dest.UserId, src => src.userId)
            .Map(dest => dest, src => src.request);

        config.NewConfig<CreatePortfolioCommand, Portfolio>();

        config.NewConfig<Portfolio, PortfolioResult>();

        config.NewConfig<PortfolioResult, PortfolioResponse>();
    }
}