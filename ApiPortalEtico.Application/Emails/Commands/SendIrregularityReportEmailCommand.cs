using System.Threading;
using System.Threading.Tasks;
using ApiPortalEtico.Application.Common.Interfaces;
using ApiPortalEtico.Application.Common.Models;
using ApiPortalEtico.Application.Emails.Templates;
using MediatR;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;

namespace ApiPortalEtico.Application.Emails.Commands
{
    public class SendIrregularityReportEmailCommand : IRequest<EmailResult>
    {
        public int IrregularityReportId { get; set; }
        public List<string> Recipients { get; set; } = new List<string>();
        public string AdditionalMessage { get; set; }
    }

    public class EmailResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class SendIrregularityReportEmailCommandHandler : IRequestHandler<SendIrregularityReportEmailCommand, EmailResult>
    {
        private readonly IApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public SendIrregularityReportEmailCommandHandler(IApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<EmailResult> Handle(SendIrregularityReportEmailCommand request, CancellationToken cancellationToken)
        {
            // Validate request
            if (request.Recipients == null || request.Recipients.Count == 0)
            {
                return new EmailResult { Success = false, Message = "No recipients specified" };
            }

            // Get the irregularity report
            var report = await _context.IrregularityReports
                .Include(r => r.Ubicacion)
                .Include(r => r.Evidencia)
                .Include(r => r.Involucrados)
                .FirstOrDefaultAsync(r => r.Id == request.IrregularityReportId, cancellationToken);

            if (report == null)
            {
                return new EmailResult { Success = false, Message = $"Irregularity report with ID {request.IrregularityReportId} not found" };
            }

            // Generate email body using the template service
            string emailBody = EmailTemplateService.GenerateIrregularityReportEmailTemplate(
                report,
                request.AdditionalMessage ?? "Gracias por su reporte. A continuación encontrará una copia de la información que ha proporcionado."
            );

            // Create email message
            var emailMessage = new EmailMessage
            {
                Subject = $"Confirmación de Reporte de Irregularidad: {report.TipoIrregularidad}",
                Body = emailBody,
                IsHtml = true
            };

            // Add recipients
            foreach (var recipient in request.Recipients)
            {
                emailMessage.To.Add(new EmailRecipient(recipient));
            }

            // Send email
            bool result = await _emailService.SendEmailAsync(emailMessage);

            return new EmailResult
            {
                Success = result,
                Message = result ? "Email sent successfully" : "Failed to send email"
            };
        }
    }
}

