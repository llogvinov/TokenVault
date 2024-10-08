using MapsterMapper;
using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.Cryptocurrencies.Common;

namespace TokenVault.Application.Features.Cryptocurrencies.Queries.GetCryptocurrencyById;

public class GetCryptocurrencyByIdQueryHandler : 
    IRequestHandler<GetCryptocurrencyByIdQuery, CryptocurrencyResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCryptocurrencyByIdQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CryptocurrencyResult> Handle(
        GetCryptocurrencyByIdQuery query,
        CancellationToken cancellationToken)
    {
        var cryptocurrency = await _unitOfWork.Cryptocurrency.GetCryptocurrencyByIdAsync(query.CryptocurrencyId);
        if (cryptocurrency == null)
        {
            throw new ArgumentNullException(nameof(cryptocurrency), 
                $"Cryptocurrency with given id: {query.CryptocurrencyId} does not exist");
        }

        var cryptocurrencyResult = _mapper.Map<CryptocurrencyResult>(cryptocurrency);
        return cryptocurrencyResult;
    }
}
