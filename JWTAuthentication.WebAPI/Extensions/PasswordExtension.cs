namespace JWTAuthentication.WebAPI.Extensions;

public static class PasswordExtension
{
    private const string SALT = "$2a$11$eRXctzGQsaDFWOnCT8e93eke";

    public static string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password, SALT);

    public static bool VerifyPassword(string password, string storedHash) => BCrypt.Net.BCrypt.Verify(password, storedHash);
}