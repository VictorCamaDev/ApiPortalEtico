using System.Threading;
using System.Threading.Tasks;
using MediatR;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using ApiPortalEtico.Application.Common.Interfaces;
using ApiPortalEtico.Application.Common.Models;
using ApiPortalEtico.Domain.Entities;

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

            // Create email message
            var emailMessage = new EmailMessage
            {
                Subject = $"Confirmación de Reporte de Irregularidad: {report.TipoIrregularidad}",
                Body = GenerateEmailBody(report, request.AdditionalMessage),
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

        private string GenerateEmailBody(IrregularityReport report, string additionalMessage)
        {
            // Create a well-formatted HTML email with the report details
            string body = $@"
            <!DOCTYPE html>
            <html>
            <head>
                <style>
                    body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
                    .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
                    h1 {{ color: #d9534f; }}
                    h2 {{ color: #5bc0de; margin-top: 20px; }}
                    .section {{ margin-bottom: 20px; }}
                    .label {{ font-weight: bold; }}
                    table {{ border-collapse: collapse; width: 100%; }}
                    th, td {{ border: 1px solid #ddd; padding: 8px; text-align: left; }}
                    th {{ background-color: #f2f2f2; }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <h1>Confirmación de Reporte de Irregularidad</h1>
                    
                    {(string.IsNullOrEmpty(additionalMessage) ? "" : $@"
                    <div class='section'>
                        <p>{additionalMessage}</p>
                    </div>
                    <hr/>")}
                    
                    <div class='section'>
                        <h2>Información General</h2>
                        <p><span class='label'>Tipo de Irregularidad:</span> {report.TipoIrregularidad}</p>
                        <p><span class='label'>Fecha:</span> {report.Fecha.ToString("dd/MM/yyyy")}</p>
                        <p><span class='label'>Detalles:</span> {report.Detalles}</p>
                    </div>
                    
                    <div class='section'>
                        <h2>Ubicación</h2>
                        <p><span class='label'>País:</span> {report.Ubicacion.Pais}</p>
                        <p><span class='label'>Provincia:</span> {report.Ubicacion.Provincia}</p>
                        <p><span class='label'>Ciudad:</span> {report.Ubicacion.Ciudad}</p>
                        <p><span class='label'>Sede:</span> {report.Ubicacion.Sede}</p>
                    </div>
                    
                    <div class='section'>
                        <h2>Involucrados</h2>
                        <table>
                            <tr>
                                <th>Nombre</th>
                                <th>Apellido</th>
                                <th>Relación</th>
                            </tr>";

            foreach (var involucrado in report.Involucrados)
            {
                body += $@"
                            <tr>
                                <td>{involucrado.Nombre}</td>
                                <td>{involucrado.Apellido}</td>
                                <td>{involucrado.Relacion}</td>
                            </tr>";
            }

            body += $@"
                        </table>
                    </div>
                    
                    <div class='section'>
                        <h2>Evidencia</h2>
                        <p><span class='label'>Tipo:</span> {report.Evidencia.Tipo}</p>
                        <p><span class='label'>Dónde Obtener:</span> {report.Evidencia.DondeObtener}</p>
                        <p><span class='label'>Entrega Física:</span> {report.Evidencia.EntregaFisica}</p>
                    </div>
                    
                    <div class='section'>
                        <h2>Información Adicional</h2>
                        <p><span class='label'>Beneficios:</span> {report.Beneficios}</p>
                        <p><span class='label'>Testigos:</span> {report.Testigos}</p>
                        <p><span class='label'>Conocimiento:</span> {report.Conocimiento}</p>
                        <p><span class='label'>Involucra Externos:</span> {report.InvolucraExternos}</p>
                        {(report.InvolucraExternos == "si" ? $"<p><span class='label'>Quiénes Externos:</span> {report.QuienesExternos}</p>" : "")}
                    </div>
                    
                    <hr/>
                    <p>Gracias por reportar esta irregularidad. Su reporte ha sido registrado con el número de referencia #{report.Id}.</p>
                    <p>Si tiene alguna pregunta o necesita proporcionar información adicional, por favor contáctenos.</p>
                </div>
            </body>
            </html>";

            return body;
        }
    }
}

