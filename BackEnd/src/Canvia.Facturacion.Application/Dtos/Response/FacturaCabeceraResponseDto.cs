using Canvia.Facturacion.Application.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canvia.Facturacion.Application.Dtos.Response
{
    public class FacturaCabeceraResponseDto
    {
        public int FacturaID { get; set; }
        public int ClienteID { get; set; }
        public DateTime FechaEmision { get; set; }
        public string? Descripcion { get; set; }
        public decimal Total { get; set; }
        public byte Estado { get; set; }
        public List<FacturaDetalleResponseDto> Items { get; set; } = new List<FacturaDetalleResponseDto>();
    }
    public class FacturaDetalleResponseDto
    {
        public int DetalleID { get; set; }
        public string? Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
