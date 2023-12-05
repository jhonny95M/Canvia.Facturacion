using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canvia.Facturacion.Application.Dtos.Request
{
    public class FacturaCabeceraRequestDto
    {
        public int ClienteID { get; set; }
        public DateTime FechaEmision { get; set; }
        public string? Descripcion { get; set; }
        public decimal Total => Items.Sum(c => c.Cantidad * c.PrecioUnitario);
        public List<FacturaDetalleRequestDto> Items { get; set; } = new List<FacturaDetalleRequestDto>();

    }
    public class FacturaDetalleRequestDto
    {
        public string? Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
