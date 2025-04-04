using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApiPortalEtico.Application.Common.Interfaces;
using ApiPortalEtico.Application.IrregularityReports;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApiPortalEtico.Application.IrregularityReports.Queries
{
    public class GetIrregularityReportByIdQuery : IRequest<IrregularityReportDto>
    {
        public int Id { get; set; }
    }

    public class GetIrregularityReportByIdQueryHandler : IRequestHandler<GetIrregularityReportByIdQuery, IrregularityReportDto>
    {
        private readonly IApplicationDbContext _context;

        public GetIrregularityReportByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IrregularityReportDto> Handle(GetIrregularityReportByIdQuery request, CancellationToken cancellationToken)
        {
            var report = await _context.IrregularityReports
                .Include(r => r.Ubicacion)
                .Include(r => r.Evidencia)
                .Include(r => r.Involucrados)
                .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

            if (report == null)
                return null;

            return new IrregularityReportDto
            {
                Id = report.Id,
                TipoIrregularidad = report.TipoIrregularidad,
                Fecha = report.Fecha,
                Detalles = report.Detalles,
                Beneficios = report.Beneficios,
                Testigos = report.Testigos,
                Conocimiento = report.Conocimiento,
                InvolucraExternos = report.InvolucraExternos,
                QuienesExternos = report.QuienesExternos,
                Ocultado = report.Ocultado,
                ComoOcultado = report.ComoOcultado,
                QuienesOcultan = report.QuienesOcultan,
                ConocimientoPrevio = report.ConocimientoPrevio,
                QuienesConocen = report.QuienesConocen,
                ComoConocen = report.ComoConocen,
                Relacion = report.Relacion,
                CorreoContacto = report.CorreoContacto,
                RelacionGrupo = report.RelacionGrupo,
                Anonimo = report.Anonimo,
                CreatedAt = report.CreatedAt,
                Ubicacion = new UbicacionDto
                {
                    Pais = report.Ubicacion.Pais,
                    Provincia = report.Ubicacion.Provincia,
                    Ciudad = report.Ubicacion.Ciudad,
                    Sede = report.Ubicacion.Sede
                },
                Evidencia = new EvidenciaDto
                {
                    Tipo = report.Evidencia.Tipo,
                    DondeObtener = report.Evidencia.DondeObtener,
                    EntregaFisica = report.Evidencia.EntregaFisica
                },
                Involucrados = report.Involucrados.Select(i => new InvolucradoDto
                {
                    Id = i.Id,
                    Nombre = i.Nombre,
                    Apellido = i.Apellido,
                    Relacion = i.Relacion,
                    Otro = i.Otro
                }).ToList()
            };
        }
    }
}

