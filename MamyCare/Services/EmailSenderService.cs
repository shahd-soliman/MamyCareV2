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
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));
        message.Sender = new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail);
        message.To.Add(MailboxAddress.Parse(email));
        message.Subject = subject;

        var builder = new BodyBuilder
        {
            HtmlBody = htmlMessage
        };
        message.Body = builder.ToMessageBody();

        using var smtp = new SmtpClient();
        smtp.CheckCertificateRevocation = false;

        _logger.LogInformation("📧 Sending email to {email}", email);

        await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
        await smtp.SendAsync(message);
        await smtp.DisconnectAsync(true);
    }
}
