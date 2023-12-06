using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canvia.Facturacion.Infraestructure.Dtos
{
    public record FacturaCabeceraEntityDto
    {
        public int FacturaId { get; set; }
        public int? ClienteId { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public DateTime? FechaEmision { get; set; }
        public string? Descripcion { get; set; }
        public decimal? Total { get; set; }
        public byte Estado { get; set; }
    }
}
