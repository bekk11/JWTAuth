using JWTAuthentication.WebAPI.Entities;
using JWTAuthentication.WebAPI.Extensions;
using JWTAuthentication.WebAPI.Models;
using JWTAuthentication.WebAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthentication.WebAPI.Services;

public class AuthService(IUserRepository _userRepo) : IAuthService
{
    public async Task<IActionResult> AuthLogin(LoginModel model)
    {
        User? user = await _userRepo.UserGetByUsername(model.Username);

        if (user is null)
            return new UnauthorizedObjectResult("Username not found!");

        if (!PasswordExtension.VerifyPassword(password: model.Password, storedHash: user.Password))
            return new UnauthorizedObjectResult("Incorrect password!");
        
        
    }

    public Task<IActionResult> AuthRefresh(RefreshModel model)
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> AuthRevoke(RefreshModel model)
    {
        throw new NotImplementedException();
    }
}