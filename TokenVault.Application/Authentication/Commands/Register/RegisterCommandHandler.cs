using MapsterMapper;
using MediatR;
using TokenVault.Application.Authentication.Common;
using TokenVault.Application.Common.Interfaces.Authentication;
using TokenVault.Application.Common.Interfaces.Persistence;
using TokenVault.Domain.Entities;

namespace TokenVault.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthenticationResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterCommandHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AuthenticationResult> Handle(
        RegisterCommand command,
        CancellationToken cancellationToken)
    {
        var userFromDb = await _unitOfWork.User.GetFirstOrDefaultAsync(
            u => u.Email == command.Email);
        if (userFromDb is not null)
        {
            throw new Exception("User with given email already exists");
        }

        var hasher = new Hasher();
        var hashedPassword = hasher.ComputeSha256Hash(command.Password);

        var user = _mapper.Map<User>((command, hashedPassword));
        await _unitOfWork.User.AddAsync(user);

        var token = _jwtTokenGenerator.GenerateToken(user);
        
        var result = new AuthenticationResult(user, token);
        return result;
    }
}