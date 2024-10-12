using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Common.Models;

namespace SyriacSources.Backend.Infrastructure.Services;
public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly JWTToken _jwtToken;

    public TokenService(IConfiguration configuration, IOptions<JWTToken> jwtToken)
    {
        _configuration = configuration;
        _jwtToken = jwtToken.Value;      
    }

    public string CreateJwtSecurityToken(string id)
    {
        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtToken.Secret));

        var token = new JwtSecurityToken(
            issuer: _jwtToken.ValidIssuer,
            audience: _jwtToken.ValidAudience,
            expires: DateTime.Now.AddHours(2),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
