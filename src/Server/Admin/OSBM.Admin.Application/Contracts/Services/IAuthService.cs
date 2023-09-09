using OSBM.Admin.Domain.Identities;

using System.IdentityModel.Tokens.Jwt;

namespace OSBM.Admin.Application.Contracts.Services;

public interface IAuthService
{
    Task<Tuple<bool, string>> Register(string email, string userName, string password, CancellationToken cancellationToken = default);

    Task<JwtSecurityToken> GetToken(AppUser user, CancellationToken cancellationToken = default);
}