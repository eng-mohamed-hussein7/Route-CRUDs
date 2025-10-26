namespace Application.Helpers;

public class EmailConfig
{
    public string CurrentProvider { get; set; } = string.Empty;
    public Dictionary<string, EmailProvider> Providers { get; set; } = new();
}

public class EmailProvider
{
    public string SmtpServer { get; set; } = string.Empty;
    public int SmtpPort { get; set; }
    public string SenderName { get; set; } = string.Empty;
    public string SenderEmail { get; set; } = string.Empty;
    public string SenderPassword { get; set; } = string.Empty;
}
