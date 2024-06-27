using MapsterMapper;
using MediatR;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Application.Features.Cryptocurrencies.Common;

namespace TokenVault.Application.Features.Cryptocurrencies.Commands.DeleteCryptocurrency;

public class DeleteCryptocurrencyCommandHandler : 
    IRequestHandler<DeleteCryptocurrencyCommand, CryptocurrencyResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeleteCryptocurrencyCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CryptocurrencyResult> Handle(
        DeleteCryptocurrencyCommand command,
        CancellationToken cancellationToken)
    {
        var cryptocurrency = await _unitOfWork.Cryptocurrency.GetFirstOrDefaultAsync(
            c => c.Id == command.CryptocurrencyId);
        if (cryptocurrency is null)
        {
            throw new ArgumentNullException(nameof(cryptocurrency), 
                $"Cryptocurrency with given id: {command.CryptocurrencyId} does not exist");
        }
        _unitOfWork.Cryptocurrency.Remove(cryptocurrency);

        var cryptocurrencyResult = _mapper.Map<CryptocurrencyResult>(cryptocurrency);
        return cryptocurrencyResult;
    }
}