using MediatR;

using Microsoft.AspNetCore.Identity;

using OSBM.Admin.Application.Contracts.Services;
using OSBM.Admin.Domain.Identities;

using System.IdentityModel.Tokens.Jwt;

namespace OSBM.Admin.Application.Features.Auth.Commands;

public class LoginCommand : IRequest<LoginResponse>
{
    public string UserName { get; set; }
    public string Password { get; set; }
}

public class LoginResponse
{
    public string Token { get; set; }
    public DateTime ExpiredAt { get; set; }
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse?>
{
    private readonly IAuthService _authService;
    private readonly UserManager<AppUser> _userManager;

    public LoginCommandHandler(IAuthService authService, UserManager<AppUser> userManager)
    {
        _authService = authService;
        _userManager = userManager;
    }

    public async Task<LoginResponse?> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user == null)
        {
            return null;
        }
        var token = await _authService.GetToken(user, cancellationToken);
        var response = new LoginResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiredAt = token.ValidTo
        };
        return response;
    }
}