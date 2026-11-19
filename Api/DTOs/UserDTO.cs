using System.ComponentModel.DataAnnotations;

namespace Api.DTOs
{
    public class UserRegisterDTO
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must not be less than 8.")]
        [MaxLength(100, ErrorMessage = "Password must not be more than 100.")]
        public string Password { get; set; }
    }

    public class UserLoginDTO
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must not be less than 8.")]
        [MaxLength(100, ErrorMessage = "Password must not be more than 100.")]
        public string Password { get; set; }
    }

    public class AuthResponseDTO
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime AccessTokenExpiresAt { get; set; }
    }

    public class RefreshTokenRequestTO
    {
        [Required(ErrorMessage = "Refresh Token is required.")]
        public string RefreshToken { get; set; }
    }
}
