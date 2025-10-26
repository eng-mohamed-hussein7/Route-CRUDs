using Application.Helpers;
using Application.IServices.ITwilioServices;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Exceptions;
using Twilio.Rest.Api.V2010.Account;

namespace Infrastructure.Services.TwilioServices;

public class TwilioService : ITwilioService
{
    private readonly TwilioSettings _settings;
    private readonly ILogger<TwilioService> _logger;
    private readonly bool _isConfigured;

    public TwilioService(IOptions<TwilioSettings> settings, ILogger<TwilioService> logger)
    {
        _settings = settings.Value;
        _logger = logger;

        if (!string.IsNullOrWhiteSpace(_settings.AccountSID) &&
            !string.IsNullOrWhiteSpace(_settings.AuthToken))
        {
            TwilioClient.Init(_settings.AccountSID, _settings.AuthToken);
            _isConfigured = true;
        }
        else
        {
            _logger.LogWarning("Twilio configuration missing or incomplete. SMS service will be disabled.");
            _isConfigured = false;
        }
    }

    public async Task<(bool IsSuccess, string Message)> SendSmsAsync(string phoneNumber, string message)
    {
        try
        {
            if (!_isConfigured)
                return (false, "Twilio service not configured properly.");

            if (string.IsNullOrWhiteSpace(phoneNumber) || string.IsNullOrWhiteSpace(message))
                return (false, "Recipient number or message cannot be empty.");

            var response = await MessageResource.CreateAsync(
                body: message,
                from: new Twilio.Types.PhoneNumber(_settings.TwilioPhoneNumber),
                to: new Twilio.Types.PhoneNumber(phoneNumber)
            );

            if (response == null)
            {
                _logger.LogWarning("Twilio response was null for {PhoneNumber}", phoneNumber);
                return (false, "Failed to send SMS — no response received.");
            }

            if (!string.IsNullOrEmpty(response.ErrorMessage))
            {
                _logger.LogError("Twilio error for {PhoneNumber}: {Error}", phoneNumber, response.ErrorMessage);
                return (false, $"Twilio error: {response.ErrorMessage}");
            }

            _logger.LogInformation("SMS sent successfully to {PhoneNumber}", phoneNumber);
            return (true, "SMS sent successfully.");
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, "Twilio API exception for {PhoneNumber}", phoneNumber);
            return (false, $"Twilio API error: {ex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while sending SMS to {PhoneNumber}", phoneNumber);
            return (false, $"Unexpected error: {ex.Message}");
        }
    }
}
