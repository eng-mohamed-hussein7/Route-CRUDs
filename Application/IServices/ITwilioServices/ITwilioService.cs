namespace Application.IServices.ITwilioServices;

public interface ITwilioService
{
    Task<(bool IsSuccess, string Message)> SendSmsAsync(string to, string message);
}
