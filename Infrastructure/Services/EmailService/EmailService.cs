using Application.IServices.IEmailService;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Options;
using Application.Helpers;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.EmailService;

public class EmailService : IEmailService
{
    private readonly EmailProvider _activeProvider;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IOptions<EmailConfig> options, ILogger<EmailService> logger)
    {
        var config = options.Value ?? throw new ArgumentNullException(nameof(options));
        if (!config.Providers.TryGetValue(config.CurrentProvider, out var provider))
            throw new InvalidOperationException($"Email provider '{config.CurrentProvider}' not found in configuration.");

        _activeProvider = provider;
        _logger = logger;
    }
    public async Task SendEmailAsync(string toEmail, string subject, string body, CancellationToken cancellationToken = default)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress(_activeProvider.SenderName, _activeProvider.SenderEmail));
        email.To.Add(MailboxAddress.Parse(toEmail));
        email.Subject = subject;

        var builder = new BodyBuilder { HtmlBody = body };
        email.Body = builder.ToMessageBody();

        using var smtp = new SmtpClient();
        try
        {

            await smtp.ConnectAsync(_activeProvider.SmtpServer, _activeProvider.SmtpPort, SecureSocketOptions.StartTls, cancellationToken);
            await smtp.AuthenticateAsync(_activeProvider.SenderEmail, _activeProvider.SenderPassword, cancellationToken);
            await smtp.SendAsync(email, cancellationToken);
            await smtp.DisconnectAsync(true, cancellationToken);
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to send email please try again later!");
        }
    }
}
