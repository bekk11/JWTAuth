using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JWTAuthentication.WebAPI.Models;
using JWTAuthentication.WebAPI.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JWTAuthentication.WebAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AuthController(IConfiguration _configuration, IAuthService authService) : ControllerBase
{
    private static readonly Dictionary<string, string> _refreshTokens = new();

    [HttpPost]
    public IActionResult Login([FromBody] LoginModel model)
    {
        if (model is not { Username: "demo", Password: "password" }) return Unauthorized("Invalid credentials");

        var accessToken = GenerateAccessToken(model.Username);
        var refreshToken = Guid.NewGuid().ToString();

        _refreshTokens[refreshToken] = model.Username;

        return Ok(new
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
            RefreshToken = refreshToken
        });
    }

    [HttpPost]
    public IActionResult Refresh([FromBody] RefreshModel request)
    {
        if (!_refreshTokens.TryGetValue(request.RefreshToken, out var userId))
            return BadRequest("Invalid refresh token");

        var token = GenerateAccessToken(userId);

        return Ok(new { AccessToken = new JwtSecurityTokenHandler().WriteToken(token) });
    }

    [HttpPost]
    public IActionResult Revoke([FromBody] RefreshModel request)
    {
        if (!_refreshTokens.ContainsKey(request.RefreshToken)) return BadRequest("Invalid refresh token");
        _refreshTokens.Remove(request.RefreshToken);
        return Ok("Token revoked successfully");
    }
    
    private JwtSecurityToken GenerateAccessToken(string userName)
    {
        var claims = new List<Claim>
        {
            new("username", userName)
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(1), // Token expiration time
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]!)),
                SecurityAlgorithms.HmacSha256)
        );

        return token;
    }
}