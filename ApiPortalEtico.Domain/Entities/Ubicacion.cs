namespace ApiPortalEtico.Domain.Entities
{
    public class Ubicacion
    {
        public int Id { get; set; }
        public string? Pais { get; set; }
        public string? Provincia { get; set; }
        public string? Ciudad { get; set; }
        public string? Sede { get; set; }
        public int IrregularityReportId { get; set; }
        public IrregularityReport IrregularityReport { get; set; }
    }
}

