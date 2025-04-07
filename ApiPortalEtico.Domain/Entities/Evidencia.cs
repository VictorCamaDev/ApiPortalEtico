namespace ApiPortalEtico.Domain.Entities
{
    public class Evidencia
    {
        public int Id { get; set; }
        public string? Tipo { get; set; }
        public string? DondeObtener { get; set; }
        public string? EntregaFisica { get; set; }
        public int IrregularityReportId { get; set; }
        public IrregularityReport IrregularityReport { get; set; }
    }
}

