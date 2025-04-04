using ApiPortalEtico.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;

namespace ApiPortalEtico.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<IrregularityReport> IrregularityReports { get; set; }
        DbSet<Involucrado> Involucrados { get; set; }
        DbSet<Ubicacion> Ubicaciones { get; set; }
        DbSet<Evidencia> Evidencias { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

