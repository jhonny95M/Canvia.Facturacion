namespace Canvia.Facturacion.Domain.EntitiesEntityFramework;

public partial class FacturaCabecera
{
    public FacturaCabecera()
    {
        FacturaDetalles = new HashSet<FacturaDetalle>();
    }

    public int? ClienteId { get; set; }
    public DateTime? FechaEmision { get; set; }
    public decimal? Total { get; set; }
    public int FacturaId { get; set; }
    public byte Estado { get; set; }
    public string? Descripcion { get; set; }

    public virtual Cliente? Cliente { get; set; }
    public virtual ICollection<FacturaDetalle> FacturaDetalles { get; set; }
}
