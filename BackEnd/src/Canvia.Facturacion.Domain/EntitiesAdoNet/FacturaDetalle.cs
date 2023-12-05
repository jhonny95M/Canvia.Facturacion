namespace Canvia.Facturacion.Domain.EntitiesAdoNet;

public class FacturaDetalle
{
    public int DetalleID { get; set; }
    public int FacturaID { get; set; }
    public string? Descripcion { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
}
