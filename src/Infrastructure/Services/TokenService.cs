using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Application.User;
using SyriacSources.Backend.Domain.Constants;

namespace EventManager.Backend.Infrastructure.Services;
public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly IApplicationPermissionService _policyService;
    private readonly IApplicationRoleService _roleService;
    private readonly JWTToken _jwtToken;

    public TokenService(IConfiguration configuration, IApplicationPermissionService policyService, IApplicationRoleService roleService, IOptions<JWTToken> jwtToken)
    {
        _configuration = configuration;
        _jwtToken = jwtToken.Value;
        _roleService = roleService;
        _policyService = policyService;
    }

    public async Task<string> CreateJwtSecurityToken(UserBasicDetailsVm details)
    {
        if (String.IsNullOrEmpty(details.Roles))
        {
            return "";
        }

        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, details.Id.ToString()!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        // Add roles to claims
        List<int> roleIds = details.Roles.Split(',').Select(int.Parse).ToList<int>();

        var roles = await _roleService.GetRolesAsync(roleIds, new CancellationToken());

        if (roles != null)
        {
            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role.NormalizedRoleName));
            }
        }

        // Add policies as claims
        var policies = await _policyService.FetchPoliciesByRolesAsync(roleIds);

        if (policies != null)
        {
            foreach (var policy in policies)
            {
                authClaims.Add(new Claim(CustomClaimTypes.Permission, policy));
            }

        }

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Convert.ToBase64String(Encoding.UTF8.GetBytes(_jwtToken.Secret))));

        var token = new JwtSecurityToken(
            issuer: _jwtToken.ValidIssuer,
            audience: _jwtToken.ValidAudience,
            expires: DateTime.Now.AddHours(_jwtToken.ExpiryByHours),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string EncodeKey(string key)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(key);
        return System.Convert.ToBase64String(plainTextBytes);

    }
}
