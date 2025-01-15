using System.Data;
using Npgsql;

namespace JWTAuthentication.WebAPI.Repositories.DBContext;

public class AppDbContext(IConfiguration configuration)
{
    private readonly NpgsqlDataSource _dataSource = NpgsqlDataSource.Create(configuration.GetConnectionString("PostgresConn")!);
    
    public async Task<IDbConnection> CreateConnectionAsync()
    {
        return await _dataSource.OpenConnectionAsync();
    }
}