using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canvia.Facturacion.Application.Dtos.Response
{
    public record ClienteSelectResponseDto
    {
        public int ClienteID { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
    }
}
