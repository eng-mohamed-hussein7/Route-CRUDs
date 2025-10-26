using Application.ResultFolder;

namespace Application.IServices.IAuthServices;

public interface IPhoneVerificationService
{
    Task<Result> AskConfirmPhoneNumberAsync(string userId);
    Task<Result> ConfirmPhoneNumberAsync(string otp, string userId);
    Task<Result> ChangePhoneNumberAsync(string newPhoneNumber, string userId);
}