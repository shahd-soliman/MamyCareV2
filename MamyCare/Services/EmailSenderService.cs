using MailKit.Net.Smtp;
using MailKit.Security;
using MamyCare.Seetings;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;

public class EmailSenderService : IEmailSender
{
    private readonly EmailSettings _mailSettings;
    private readonly ILogger<EmailSenderService> _logger;

    public EmailSenderService(IOptionsSnapshot<EmailSettings> mailSettings, ILogger<EmailSenderService> logger)
    {
        if (mailSettings.Value == null)
        {
            throw new ArgumentNullException(nameof(mailSettings), "❌ EmailSettings is null! Check appsettings.json.");
        }

        _mailSettings = mailSettings.Value;
        _logger = logger;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var message = new MimeMessage
        {
            Sender = MailboxAddress.Parse(_mailSettings.Mail),
            Subject = subject
        };

        message.To.Add(MailboxAddress.Parse(email));

        var builder = new BodyBuilder
        {
            HtmlBody = htmlMessage
        };

        message.Body = builder.ToMessageBody();

        using var smtp = new SmtpClient();
        
        _logger.LogInformation("Sending email to {email}", email);
        smtp.CheckCertificateRevocation = false;
        smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
        smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
        await smtp.SendAsync(message);
        smtp.Disconnect(true);
    }
}
