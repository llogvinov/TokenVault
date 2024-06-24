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
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public RegisterCommandHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository,
        IMapper mapper)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<AuthenticationResult> Handle(
        RegisterCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
        if (_userRepository.GetUserByEmail(command.Email) is not null)
        {
            throw new Exception("User with given email already exists");
        }

        var user = _mapper.Map<User>(command);
        _userRepository.Add(user);

        var token = _jwtTokenGenerator.GenerateToken(user);
        
        var result = new AuthenticationResult(user, token);
        return result;
    }
}