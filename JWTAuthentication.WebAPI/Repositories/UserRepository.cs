using Dapper;
using JWTAuthentication.WebAPI.Entities;
using JWTAuthentication.WebAPI.Repositories.DBContext;

namespace JWTAuthentication.WebAPI.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task<User?> UserGetByUsername(string username)
    {
        const string sql = "select * from t_user where username = @Username";
        using var connection = await context.CreateConnectionAsync();
        return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Username = username });
    }

    public async Task<User?> UserGetById(long id)
    {
        const string sql = "select * from t_user where id = @Id";
        using var connection = await context.CreateConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<User>(sql, new { Id = id });
    }

    public async Task<bool> UserExists(string username)
    {
        const string sql = "select exists(select from t_user where username = @Username)";
        using var connection = await context.CreateConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<bool>(sql, new { Username = username });
    }

    public async Task<int> UserAdd(string username, string password)
    {
        const string sql = "insert into t_user(username, password) values(@Username, @Password)";
        using var connection = await context.CreateConnectionAsync();
        return await connection.ExecuteAsync(sql, new { Username = username, Password = password });
    }

    public Task<int> UserUpdateRefresh(long userId, string refreshToken)
    {
        throw new NotImplementedException();
    }
}