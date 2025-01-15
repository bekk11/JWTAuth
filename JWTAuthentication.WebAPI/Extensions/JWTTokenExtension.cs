using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JWTAuthentication.WebAPI.Enums;
using Microsoft.IdentityModel.Tokens;

namespace JWTAuthentication.WebAPI.Extensions;

public static class JWTTokenExtension
{
    private static string _GenerateAccessToken(IConfiguration configuration, string username, Role? role = null)
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                configuration.GetValue<string>("Jwt:SecretKey") ??
                throw new InvalidOperationException()
            )
        );

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, username),
            new(ClaimTypes.Role, role.ToString() ?? string.Empty)
        };

        var token = new JwtSecurityToken(
            configuration["JWT:Issuer"],
            configuration["JWT:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(configuration.GetValue<int>("Jwt:ExpireMinutes")),
            signingCredentials: credentials);
    }

    private static string _GenerateRefreshToken(string username, Role? role = null) => Guid.NewGuid().ToString();
}