namespace Canvia.Facturacion.Application.Dtos.Response;

public record FacturaResponseDto
{
    public int FacturaId { get; set; }
    public int? ClienteId { get; set; }
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public DateTime? FechaEmision { get; set; }
    public string? Descripcion { get; set; }
    public decimal? Total { get; set; }
    public byte Estado { get; set; }
    public string EstadoFactura { get; set; } = "";
}
