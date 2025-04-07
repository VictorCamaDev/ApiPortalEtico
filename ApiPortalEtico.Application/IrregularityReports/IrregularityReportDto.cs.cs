using System;
using System.Collections.Generic;

namespace ApiPortalEtico.Application.IrregularityReports
{
    public class IrregularityReportDto
    {
        public int Id { get; set; }
        public string? TipoIrregularidad { get; set; }
        public List<InvolucradoDto> Involucrados { get; set; } = new List<InvolucradoDto>();
        public UbicacionDto Ubicacion { get; set; }
        public DateTime Fecha { get; set; }
        public string? Detalles { get; set; }
        public EvidenciaDto Evidencia { get; set; }
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

        public DateTime CreatedAt { get; set; }
    }

    public class InvolucradoDto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Relacion { get; set; }
        public string? Otro { get; set; }
    }

    public class UbicacionDto
    {
        public string? Pais { get; set; }
        public string? Provincia { get; set; }
        public string? Ciudad { get; set; }
        public string? Sede { get; set; }
    }

    public class EvidenciaDto
    {
        public string? Tipo { get; set; }
        public string? DondeObtener { get; set; }
        public string? EntregaFisica { get; set; }
    }
}

