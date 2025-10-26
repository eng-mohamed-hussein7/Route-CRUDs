using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.AuthDTOs;

public class ResetPasswordDto
{
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}
public class VerifyOtpDto
{
    [EmailAddress]
    public string Email { get; set; }
    public string OtpCode { get; set; }
    public string NewPassword { get; set; }
}
public class OtpRequest
{
    [EmailAddress]
    public string Email { get; set; }
}
