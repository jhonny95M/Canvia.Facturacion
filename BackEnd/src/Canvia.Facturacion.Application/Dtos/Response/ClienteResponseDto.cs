namespace Canvia.Facturacion.Application.Dtos.Response;

public record ClienteResponseDto
{
    public int ClienteID { get; set; }
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public string? Email { get; set; }
}
