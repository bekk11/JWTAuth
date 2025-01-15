using JWTAuthentication.WebAPI.Entities;

namespace JWTAuthentication.WebAPI.Repositories;

public interface IUserRepository
{
    Task<User?> UserGetByUsername(string username);
    Task<User?> UserGetById(long id);
    Task<bool> UserExists(string username);
    Task<int> UserAdd(string username, string password);
    Task<int> UserUpdateRefresh(long userId, string refreshToken);
}