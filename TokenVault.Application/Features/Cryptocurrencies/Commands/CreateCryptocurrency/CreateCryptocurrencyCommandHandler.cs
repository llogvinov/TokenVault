using MapsterMapper;
using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.Cryptocurrencies.Common;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Features.Cryptocurrencies.Commands.CreateCryptocurrency;

public class CreateCryptocurrencyCommandHandler : 
    IRequestHandler<CreateCryptocurrencyCommand, CryptocurrencyResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCryptocurrencyCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CryptocurrencyResult> Handle(
        CreateCryptocurrencyCommand command,
        CancellationToken cancellationToken)
    {
        var cryptocurrency = _mapper.Map<Cryptocurrency>(command);
        await _unitOfWork.Cryptocurrency.AddAsync(cryptocurrency);

        var cryptocurrencyResult = _mapper.Map<CryptocurrencyResult>(cryptocurrency);
        return cryptocurrencyResult;
    }
}