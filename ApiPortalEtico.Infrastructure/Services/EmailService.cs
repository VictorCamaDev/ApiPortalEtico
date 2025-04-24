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
        private readonly IWebHostEnvironment _environment;

        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger, IWebHostEnvironment environment)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
            _environment = environment;
        }

        public async Task<bool> SendEmailAsync(EmailMessage emailMessage)
        {
            try
            {
                var message = new MailMessage();

                // Set sender
                message.From = new MailAddress(
                    emailMessage.FromAddress ?? _emailSettings.DefaultFromEmail,
                    emailMessage.FromName ?? _emailSettings.DefaultFromName);

                // Add recipients
                foreach (var recipient in emailMessage.To)
                {
                    message.To.Add(new MailAddress(recipient.Address, recipient.Name));
                }

                // Add CC recipients
                foreach (var recipient in emailMessage.Cc)
                {
                    message.CC.Add(new MailAddress(recipient.Address, recipient.Name));
                }
                //message.CC.Add(new MailAddress("jorge.enrique@gruposilvestre.com.pe", "Jorge Enrique"));
                //message.CC.Add(new MailAddress("july.carpio@gruposilvestre.com.pe", "July Carpio"));

                // Add BCC recipients
                foreach (var recipient in emailMessage.Bcc)
                {
                    message.Bcc.Add(new MailAddress(recipient.Address, recipient.Name));
                }

                // Set subject and body
                message.Subject = emailMessage.Subject;
                message.Body = emailMessage.Body;
                message.IsBodyHtml = emailMessage.IsHtml;

                // Add logo as linked resource
                string logoPath = null;
                if (_environment.WebRootPath != null)
                {
                    logoPath = Path.Combine(_environment.WebRootPath, "images", "logo-gruposilvestre.jpg");
                }
                else
                {
                    // Intentar obtener la ruta base de la aplicación como alternativa
                    string basePath = AppDomain.CurrentDomain.BaseDirectory;
                    logoPath = Path.Combine(basePath, "wwwroot", "images", "logo-gruposilvestre.jpg");
                    _logger.LogWarning($"WebRootPath es nulo, usando ruta alternativa: {logoPath}");
                }

                if (File.Exists(logoPath))
                {
                    _logger.LogInformation($"Logo encontrado en: {logoPath}");

                    var logoAttachment = new LinkedResource(logoPath)
                    {
                        ContentId = "logo",
                        ContentType = new System.Net.Mime.ContentType("image/jpeg")
                    };

                    AlternateView htmlView = AlternateView.CreateAlternateViewFromString(
                        emailMessage.Body,
                        null,
                        System.Net.Mime.MediaTypeNames.Text.Html);

                    htmlView.LinkedResources.Add(logoAttachment);
                    message.AlternateViews.Add(htmlView);
                }
                else
                {
                    _logger.LogWarning($"Logo no encontrado en la ruta: {logoPath}");
                    // Continuar sin el logo
                }

                // Add attachments
                foreach (var attachment in emailMessage.Attachments)
                {
                    using var ms = new System.IO.MemoryStream(attachment.Content);
                    message.Attachments.Add(new Attachment(ms, attachment.FileName, attachment.ContentType));
                }

                // Configure SMTP client
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

                // Send email
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

