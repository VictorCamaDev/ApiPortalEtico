using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using ApiPortalEtico.Application.Common.Interfaces;
using ApiPortalEtico.Application.Common.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ApiPortalEtico.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
        }

        public async Task<bool> SendEmailAsync(EmailMessage emailMessage)
        {
            try
            {
                var message = new MailMessage();

                message.From = new MailAddress(
                    emailMessage.FromAddress ?? _emailSettings.DefaultFromEmail,
                    emailMessage.FromName ?? _emailSettings.DefaultFromName);

                foreach (var recipient in emailMessage.To)
                {
                    message.To.Add(new MailAddress(recipient.Address, recipient.Name));
                }

                foreach (var recipient in emailMessage.Cc)
                {
                    message.CC.Add(new MailAddress(recipient.Address, recipient.Name));
                }

                foreach (var recipient in emailMessage.Bcc)
                {
                    message.Bcc.Add(new MailAddress(recipient.Address, recipient.Name));
                }

                message.Subject = emailMessage.Subject;
                message.Body = emailMessage.Body;
                message.IsBodyHtml = emailMessage.IsHtml;

                foreach (var attachment in emailMessage.Attachments)
                {
                    using var ms = new System.IO.MemoryStream(attachment.Content);
                    message.Attachments.Add(new Attachment(ms, attachment.FileName, attachment.ContentType));
                }

                using var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort);
                client.EnableSsl = _emailSettings.UseSsl;

                if (!string.IsNullOrEmpty(_emailSettings.SmtpUsername) && !string.IsNullOrEmpty(_emailSettings.SmtpPassword))
                {
                    client.Credentials = new NetworkCredential(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword);
                }
                else
                {
                    client.UseDefaultCredentials = true;
                }

                await client.SendMailAsync(message);
                _logger.LogInformation($"Email sent successfully to {string.Join(", ", emailMessage.To)}");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error sending email: {ex.Message}");
                return false;
            }
        }
    }

    public class EmailSettings
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public bool UseSsl { get; set; }
        public string DefaultFromEmail { get; set; }
        public string DefaultFromName { get; set; }
    }
}

