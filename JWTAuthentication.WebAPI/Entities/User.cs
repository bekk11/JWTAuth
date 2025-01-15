namespace JWTAuthentication.WebAPI.Entities;

public class User
{
    public long Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public string? RefreshToken { get; set; }
    public bool IsActive { get; set; }
}