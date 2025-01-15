namespace JWTAuthentication.WebAPI.Entities;

public class RefreshToken
{
    public long UserId { get; set; }
    public required string Token { get; set; }
    public DateTime Expiry { get; set; }
}