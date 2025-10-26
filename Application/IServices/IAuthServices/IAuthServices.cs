using Application.DTOs.AuthDTOs;
using Application.ResultFolder;

namespace Application.IServices.IAuthServices;

public interface IAuthService
{
    // Core authentication
    Task<Result> LoginAsync(LoginDto dto);
    Task<Result> RegisterAsync(RegisterDto model);

    // Password management
    Task<Result> ChangePasswordAsync(string userId, ChangePasswordDto dto);
    Task<Result> ResetPasswordAsync(VerifyOtpDto dto);
    Task<Result> ForgotPasswordAsync(string email);
    Task<Result> ResendOtpAsync(string email);
    Task<Result> AskResetPasswordByPhoneNumberAsync(string phoneNumber);
    Task<Result> ResetPasswordByPhoneNumberAsync(VerifyOtpDto dto);

    // CRUD related to users
    Task<Result> GetAllAsync();
    Task<Result> GetByIdAsync(string id);
}
