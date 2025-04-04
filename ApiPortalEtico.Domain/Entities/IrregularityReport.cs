using System;
using System.Collections.Generic;

namespace ApiPortalEtico.Domain.Entities
{
    public class IrregularityReport
    {
        public int Id { get; set; }
        public string TipoIrregularidad { get; set; }
        public List<Involucrado> Involucrados { get; set; } = new List<Involucrado>();
        public Ubicacion Ubicacion { get; set; }
        public DateTime Fecha { get; set; }
        public string Detalles { get; set; }
        public Evidencia Evidencia { get; set; }
        public string Beneficios { get; set; }
        public string Testigos { get; set; }
        public string Conocimiento { get; set; }
        public string InvolucraExternos { get; set; }
        public string? QuienesExternos { get; set; }
        public string Ocultado { get; set; }
        public string? ComoOcultado { get; set; }
        public string? QuienesOcultan { get; set; }
        public string ConocimientoPrevio { get; set; }
        public string? QuienesConocen { get; set; }
        public string? ComoConocen { get; set; }
        public string Relacion { get; set; }
        public string CorreoContacto { get; set; }
        public string RelacionGrupo { get; set; }
        public string Anonimo { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

