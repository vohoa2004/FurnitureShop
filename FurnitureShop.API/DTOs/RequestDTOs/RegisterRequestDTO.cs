using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class RegisterRequestDTO
{
    [Required]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format.")]
    public required string Email { get; set; }

    [Required]
    [MaxLength(50, ErrorMessage = "Username cannot be longer than 50 characters.")]
    [RegularExpression(@"^\S*$", ErrorMessage = "Username cannot contain spaces.")]
    public required string UserName { get; set; }

    [Required]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,50}$",
        ErrorMessage = "Password must be 8-50 characters long, contain at least one uppercase letter, one lowercase letter, one number, and one special character, and must not contain spaces.")]
    public required string Password { get; set; }
}
