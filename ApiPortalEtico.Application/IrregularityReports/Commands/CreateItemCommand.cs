using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using ApiPortalEtico.Domain.Entities;
using ApiPortalEtico.Application.Common.Interfaces;
using ApiPortalEtico.Application.IrregularityReports.Events;
using MediatR;
using ApiPortalEtico.Application.IrregularityReports;

namespace ApiPortalEtico.Application.IrregularityReports.Commands
{
    public class CreateIrregularityReportCommand : IRequest<IrregularityReportDto>
    {
        public string? TipoIrregularidad { get; set; }
        public List<InvolucradoDto> Involucrados { get; set; } = new List<InvolucradoDto>();
        public UbicacionDto Ubicacion { get; set; }
        public DateTime Fecha { get; set; }
        public string? Detalles { get; set; }
        public EvidenciaDto? Evidencia { get; set; }
        public string? Beneficios { get; set; }
        public string? Testigos { get; set; }
        public string? Conocimiento { get; set; }
        public string? InvolucraExternos { get; set; }
        public string? QuienesExternos { get; set; }
        public string? Ocultado { get; set; }
        public string? ComoOcultado { get; set; }
        public string? QuienesOcultan { get; set; }
        public string? ConocimientoPrevio { get; set; }
        public string? QuienesConocen { get; set; }
        public string? ComoConocen { get; set; }
        public string? Relacion { get; set; }

        // Correo para reportes anónimos
        public string? CorreoContacto { get; set; }

        // Correo para reportes no anónimos
        public string? Correo { get; set; }

        public string? RelacionGrupo { get; set; }
        public string? Anonimo { get; set; }

        // Campos adicionales
        public string? NombreCompleto { get; set; }
        public string? Telefono { get; set; }
        public string? OtroContacto { get; set; }
        public string? Cargo { get; set; }
        public string? Area { get; set; }
        public string? AreaOtro { get; set; }
        public bool AceptaTerminos { get; set; }
    }

    public class CreateIrregularityReportCommandHandler : IRequestHandler<CreateIrregularityReportCommand, IrregularityReportDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMediator _mediator;

        public CreateIrregularityReportCommandHandler(IApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<IrregularityReportDto> Handle(CreateIrregularityReportCommand request, CancellationToken cancellationToken)
        {
            var entity = new IrregularityReport
            {
                TipoIrregularidad = request.TipoIrregularidad,
                Fecha = request.Fecha,
                Detalles = request.Detalles,
                Beneficios = request.Beneficios,
                Testigos = request.Testigos,
                Conocimiento = request.Conocimiento,
                InvolucraExternos = request.InvolucraExternos,
                QuienesExternos = request.QuienesExternos,
                Ocultado = request.Ocultado,
                ComoOcultado = request.ComoOcultado,
                QuienesOcultan = request.QuienesOcultan,
                ConocimientoPrevio = request.ConocimientoPrevio,
                QuienesConocen = request.QuienesConocen,
                ComoConocen = request.ComoConocen,
                Relacion = request.Relacion,
                CorreoContacto = request.CorreoContacto,
                Correo = request.Correo,
                RelacionGrupo = request.RelacionGrupo,
                Anonimo = request.Anonimo,

                // Campos adicionales
                NombreCompleto = request.NombreCompleto,
                Telefono = request.Telefono,
                OtroContacto = request.OtroContacto,
                Cargo = request.Cargo,
                Area = request.Area,
                AreaOtro = request.AreaOtro,
                AceptaTerminos = request.AceptaTerminos,

                CreatedAt = DateTime.UtcNow,
                Ubicacion = new Ubicacion
                {
                    Pais = request.Ubicacion.Pais,
                    Provincia = request.Ubicacion.Provincia,
                    Ciudad = request.Ubicacion.Ciudad,
                    Sede = request.Ubicacion.Sede
                },
                Evidencia = new Evidencia
                {
                    Tipo = request.Evidencia.Tipo,
                    DondeObtener = request.Evidencia.DondeObtener,
                    EntregaFisica = request.Evidencia.EntregaFisica
                }
            };

            foreach (var involucrado in request.Involucrados)
            {
                entity.Involucrados.Add(new Involucrado
                {
                    Nombre = involucrado.Nombre,
                    Apellido = involucrado.Apellido,
                    Relacion = involucrado.Relacion,
                    Otro = involucrado.Otro
                });
            }

            _context.IrregularityReports.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            // Determinar qué correo usar para enviar la confirmación
            string emailToUse = null;

            if (entity.Anonimo?.ToLower() == "si" || entity.Anonimo?.ToLower() == "sí")
            {
                // Si es anónimo, usar el correo de contacto
                emailToUse = entity.CorreoContacto;
            }
            else
            {
                // Si no es anónimo, usar el correo principal
                emailToUse = entity.Correo;
            }

            // Enviar correo de confirmación si hay un correo disponible
            if (!string.IsNullOrEmpty(emailToUse))
            {
                // Publish event that a new irregularity report was created
                await _mediator.Publish(new IrregularityReportCreatedEvent
                {
                    IrregularityReportId = entity.Id,
                    EmailToUse = emailToUse
                }, cancellationToken);
            }

            // Map back to DTO
            var dto = new IrregularityReportDto
            {
                Id = entity.Id,
                TipoIrregularidad = entity.TipoIrregularidad,
                Fecha = entity.Fecha,
                Detalles = entity.Detalles,
                Beneficios = entity.Beneficios,
                Testigos = entity.Testigos,
                Conocimiento = entity.Conocimiento,
                InvolucraExternos = entity.InvolucraExternos,
                QuienesExternos = entity.QuienesExternos,
                Ocultado = entity.Ocultado,
                ComoOcultado = entity.ComoOcultado,
                QuienesOcultan = entity.QuienesOcultan,
                ConocimientoPrevio = entity.ConocimientoPrevio,
                QuienesConocen = entity.QuienesConocen,
                ComoConocen = entity.ComoConocen,
                Relacion = entity.Relacion,
                CorreoContacto = entity.CorreoContacto,
                Correo = entity.Correo,
                RelacionGrupo = entity.RelacionGrupo,
                Anonimo = entity.Anonimo,

                // Campos adicionales
                NombreCompleto = entity.NombreCompleto,
                Telefono = entity.Telefono,
                OtroContacto = entity.OtroContacto,
                Cargo = entity.Cargo,
                Area = entity.Area,
                AreaOtro = entity.AreaOtro,
                AceptaTerminos = entity.AceptaTerminos,

                CreatedAt = entity.CreatedAt,
                Ubicacion = new UbicacionDto
                {
                    Pais = entity.Ubicacion.Pais,
                    Provincia = entity.Ubicacion.Provincia,
                    Ciudad = entity.Ubicacion.Ciudad,
                    Sede = entity.Ubicacion.Sede
                },
                Evidencia = new EvidenciaDto
                {
                    Tipo = entity.Evidencia.Tipo,
                    DondeObtener = entity.Evidencia.DondeObtener,
                    EntregaFisica = entity.Evidencia.EntregaFisica
                }
            };

            foreach (var involucrado in entity.Involucrados)
            {
                dto.Involucrados.Add(new InvolucradoDto
                {
                    Id = involucrado.Id,
                    Nombre = involucrado.Nombre,
                    Apellido = involucrado.Apellido,
                    Relacion = involucrado.Relacion,
                    Otro = involucrado.Otro
                });
            }

            return dto;
        }
    }
}

