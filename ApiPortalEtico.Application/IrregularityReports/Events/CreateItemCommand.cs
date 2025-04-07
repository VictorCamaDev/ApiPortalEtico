using System.Threading;
using System.Threading.Tasks;
using ApiPortalEtico.Application.Common.Interfaces;
using MediatR;
using System.Collections.Generic;
using ApiPortalEtico.Application.Emails.Commands;

namespace ApiPortalEtico.Application.IrregularityReports.Events
{
    public class IrregularityReportCreatedEvent : INotification
    {
        public int IrregularityReportId { get; set; }
        public string EmailToUse { get; set; }
    }

    public class IrregularityReportCreatedEventHandler : INotificationHandler<IrregularityReportCreatedEvent>
    {
        private readonly IMediator _mediator;
        private readonly IApplicationDbContext _context;

        public IrregularityReportCreatedEventHandler(IMediator mediator, IApplicationDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public async Task Handle(IrregularityReportCreatedEvent notification, CancellationToken cancellationToken)
        {
            // Verificar que tenemos un correo al que enviar
            if (string.IsNullOrEmpty(notification.EmailToUse))
            {
                // No hay correo para enviar
                return;
            }

            // Crear comando de correo
            var emailCommand = new SendIrregularityReportEmailCommand
            {
                IrregularityReportId = notification.IrregularityReportId,
                Recipients = new List<string> { notification.EmailToUse },
                AdditionalMessage = "Gracias por su reporte. A continuación encontrará una copia de la información que ha proporcionado."
            };

            // Enviar correo
            await _mediator.Send(emailCommand, cancellationToken);
        }
    }
}

