using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.AuthDTOs;

public class RegisterDto
{
    [Required]
    public string UserName { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Required]
    [RegularExpression(@"^\+[1-9]\d{8,14}$", ErrorMessage = "Invalid phone number format.")]
    public string PhoneNumber { get; set; }

    [Required]
    public string Password { get; set; }
}
