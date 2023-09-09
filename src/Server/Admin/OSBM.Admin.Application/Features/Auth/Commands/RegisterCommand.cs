using MediatR;

using OSBM.Admin.Application.Contracts.Services;

namespace OSBM.Admin.Application.Features.Auth.Commands;

public class RegisterCommand : IRequest<RegisterResponse>
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}

public class RegisterResponse
{
    public RegisterResponse(bool isSuccess, string? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; set; }
    public string? Error { get; set; }
}

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponse>
{
    private readonly IAuthService _authService;

    public RegisterCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var result = await _authService.Register(request.Email, request.Username, request.Password, cancellationToken);
        if (result.Item1)
        {
            return new RegisterResponse(true, null);
        }
        else
        {
            return new RegisterResponse(false, result.Item2);
        }
    }
}