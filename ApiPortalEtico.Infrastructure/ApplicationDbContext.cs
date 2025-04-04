using ApiPortalEtico.Application.Common.Interfaces;
using ApiPortalEtico.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Threading;
using System.Threading.Tasks;

namespace ApiPortalEtico.Infrastructure
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<IrregularityReport> IrregularityReports { get; set; }
        public DbSet<Involucrado> Involucrados { get; set; }
        public DbSet<Ubicacion> Ubicaciones { get; set; }
        public DbSet<Evidencia> Evidencias { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IrregularityReport>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TipoIrregularidad).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Detalles).HasMaxLength(2000);

                entity.HasOne(e => e.Ubicacion)
                    .WithOne(u => u.IrregularityReport)
                    .HasForeignKey<Ubicacion>(u => u.IrregularityReportId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Evidencia)
                    .WithOne(ev => ev.IrregularityReport)
                    .HasForeignKey<Evidencia>(ev => ev.IrregularityReportId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Involucrados)
                    .WithOne(i => i.IrregularityReport)
                    .HasForeignKey(i => i.IrregularityReportId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Involucrado>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).HasMaxLength(100);
                entity.Property(e => e.Apellido).HasMaxLength(100);
                entity.Property(e => e.Relacion).HasMaxLength(100);
            });

            modelBuilder.Entity<Ubicacion>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Pais).HasMaxLength(100);
                entity.Property(e => e.Provincia).HasMaxLength(100);
                entity.Property(e => e.Ciudad).HasMaxLength(100);
                entity.Property(e => e.Sede).HasMaxLength(100);
            });

            modelBuilder.Entity<Evidencia>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Tipo).HasMaxLength(100);
                entity.Property(e => e.DondeObtener).HasMaxLength(500);
                entity.Property(e => e.EntregaFisica).HasMaxLength(100);
            });
        }
    }
}

