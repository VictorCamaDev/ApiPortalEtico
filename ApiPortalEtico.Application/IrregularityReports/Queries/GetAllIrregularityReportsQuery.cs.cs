using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApiPortalEtico.Application.Common.Interfaces;
using ApiPortalEtico.Application.IrregularityReports;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApiPortalEtico.Application.IrregularityReports.Queries
{
    public class GetAllIrregularityReportsQuery : IRequest<List<IrregularityReportDto>>
    {
    }

    public class GetAllIrregularityReportsQueryHandler : IRequestHandler<GetAllIrregularityReportsQuery, List<IrregularityReportDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllIrregularityReportsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<IrregularityReportDto>> Handle(GetAllIrregularityReportsQuery request, CancellationToken cancellationToken)
        {
            var reports = await _context.IrregularityReports
                .Include(r => r.Ubicacion)
                .Include(r => r.Evidencia)
                .Include(r => r.Involucrados)
                .ToListAsync(cancellationToken);

            return reports.Select(r => new IrregularityReportDto
            {
                Id = r.Id,
                TipoIrregularidad = r.TipoIrregularidad,
                Fecha = r.Fecha,
                Detalles = r.Detalles,
                Beneficios = r.Beneficios,
                Testigos = r.Testigos,
                Conocimiento = r.Conocimiento,
                InvolucraExternos = r.InvolucraExternos,
                QuienesExternos = r.QuienesExternos,
                Ocultado = r.Ocultado,
                ComoOcultado = r.ComoOcultado,
                QuienesOcultan = r.QuienesOcultan,
                ConocimientoPrevio = r.ConocimientoPrevio,
                QuienesConocen = r.QuienesConocen,
                ComoConocen = r.ComoConocen,
                Relacion = r.Relacion,
                CorreoContacto = r.CorreoContacto,
                RelacionGrupo = r.RelacionGrupo,
                Anonimo = r.Anonimo,
                CreatedAt = r.CreatedAt,
                Ubicacion = new UbicacionDto
                {
                    Pais = r.Ubicacion.Pais,
                    Provincia = r.Ubicacion.Provincia,
                    Ciudad = r.Ubicacion.Ciudad,
                    Sede = r.Ubicacion.Sede
                },
                Evidencia = new EvidenciaDto
                {
                    Tipo = r.Evidencia.Tipo,
                    DondeObtener = r.Evidencia.DondeObtener,
                    EntregaFisica = r.Evidencia.EntregaFisica
                },
                Involucrados = r.Involucrados.Select(i => new InvolucradoDto
                {
                    Id = i.Id,
                    Nombre = i.Nombre,
                    Apellido = i.Apellido,
                    Relacion = i.Relacion,
                    Otro = i.Otro
                }).ToList()
            }).ToList();
        }
    }
}

