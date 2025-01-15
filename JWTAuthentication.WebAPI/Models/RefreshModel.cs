using System.ComponentModel.DataAnnotations;

namespace JWTAuthentication.WebAPI.Models;

public class RefreshModel
{
    [Required(ErrorMessage = "RefreshToken is required")]
    public required string RefreshToken { get; set; }
}