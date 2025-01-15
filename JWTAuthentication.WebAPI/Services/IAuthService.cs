using JWTAuthentication.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthentication.WebAPI.Services;

public interface IAuthService
{
    Task<IActionResult> AuthLogin(LoginModel model);
    Task<IActionResult> AuthRefresh(RefreshModel model);
    Task<IActionResult> AuthRevoke(RefreshModel model);
}