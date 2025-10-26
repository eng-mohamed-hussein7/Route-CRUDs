using Application.ResultFolder;

namespace Application.IServices.IAuthServices;

public interface IEmailVerificationService
{
    Task<Result> AskConfirmEmailAddressAsync(string userId);
    Task<Result> ConfirmEmailAddressAsync(string otp, string userId);
    Task<Result> ChangeEmailAddressAsync(string newEmail, string userId);
}
/*
     public async Task<Result> AskConfirmEmailAdress(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return Result.Failure("User not found.", null, StatusResult.NotExists);

        return await GenerateAndSendOtpAsync(user, "Your OTP Code");
    }
    public async Task<Result> ConfirmEmailAdress(string otp, string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return Result.Failure("User not found.", status: StatusResult.NotExists);

        var validOtp = await VerifyOtpAsync(user, otp);
        if (!validOtp)
            return Result.Failure("Invalid or expired OTP code.", status: StatusResult.Failed);

        // Proceed to Confirm
        user.EmailConfirmed = true;
        await _userManager.UpdateAsync(user);
        await ResetOtpAsync(user);
       

        return Result.Success("Email Verified successfully.");
    }
 
 */