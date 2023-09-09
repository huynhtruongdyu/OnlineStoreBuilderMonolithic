using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using OSBM.Admin.Application.Contracts.Services;
using OSBM.Admin.Domain.Identities;
using OSBM.Admin.Shared.Constants;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OSBM.Admin.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    public async Task<JwtSecurityToken> GetToken(AppUser user, CancellationToken cancellationToken = default)
    {
        var userRoles = await _userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return token;
    }

    public async Task<Tuple<bool, string>> Register(string email, string userName, string password, CancellationToken cancellationToken = default)
    {
        var userExists = await _userManager.FindByNameAsync(userName) ?? await _userManager.FindByEmailAsync(email);
        if (userExists != null)
        {
            return Tuple.Create(false, "user.exists");
        }

        AppUser user = new()
        {
            Email = email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = userName
        };
        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            return Tuple.Create(false, "failed.to.register");
        }

        if (!await _roleManager.RoleExistsAsync(RoleConstants.ADMIN))
            await _roleManager.CreateAsync(new AppRole { Name = RoleConstants.ADMIN });
        if (!await _roleManager.RoleExistsAsync(RoleConstants.USER))
            await _roleManager.CreateAsync(new AppRole { Name = RoleConstants.USER });

        if (await _roleManager.RoleExistsAsync(RoleConstants.ADMIN))
        {
            await _userManager.AddToRoleAsync(user, RoleConstants.ADMIN);
        }
        if (await _roleManager.RoleExistsAsync(RoleConstants.ADMIN))
        {
            await _userManager.AddToRoleAsync(user, RoleConstants.USER);
        }
        return Tuple.Create(true, string.Empty);
    }
}