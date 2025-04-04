namespace ApiPortalEtico.Domain.Entities
{
    public class Involucrado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Relacion { get; set; }
        public string Otro { get; set; }
        public int IrregularityReportId { get; set; }
        public IrregularityReport IrregularityReport { get; set; }
    }
}

