namespace Canvia.Facturacion.Domain.EntitiesAdoNet;

public class FacturaCabeceraAdoNet
{
    public int FacturaID { get; set; }
    public int ClienteID { get; set; }
    public DateTime FechaEmision { get; set; }
    public decimal Total { get; set; }
    public int Estado { get; set; }
    public string? Descripcion { get; set; }
}
