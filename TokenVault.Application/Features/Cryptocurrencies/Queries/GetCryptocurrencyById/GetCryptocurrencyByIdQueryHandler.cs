using MapsterMapper;
using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.Cryptocurrencies.Common;

namespace TokenVault.Application.Features.Cryptocurrencies.Queries.GetCryptocurrencyById;

public class GetCryptocurrencyByIdQueryHandler : 
    IRequestHandler<GetCryptocurrencyByIdQuery, CryptocurrencyResult>
{
    private readonly ICryptocurrencyRepository _cryptocurrencyRepository;
    private readonly IMapper _mapper;

    public GetCryptocurrencyByIdQueryHandler(
        ICryptocurrencyRepository cryptocurrencyRepository,
        IMapper mapper)
    {
        _cryptocurrencyRepository = cryptocurrencyRepository;
        _mapper = mapper;
    }

    public async Task<CryptocurrencyResult> Handle(
        GetCryptocurrencyByIdQuery query,
        CancellationToken cancellationToken)
    {
        var cryptocurrency = await _cryptocurrencyRepository.GetCryptocurrencyByIdAsync(query.CryptocurrencyId);
        if (cryptocurrency == null)
        {
            throw new ArgumentNullException(nameof(cryptocurrency), 
                $"Cryptocurrency with given id: {query.CryptocurrencyId} does not exist");
        }

        var cryptocurrencyResult = _mapper.Map<CryptocurrencyResult>(cryptocurrency);
        return cryptocurrencyResult;
    }
}
