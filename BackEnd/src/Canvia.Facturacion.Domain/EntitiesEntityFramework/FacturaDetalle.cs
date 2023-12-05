using System;
using System.Collections.Generic;

namespace Canvia.Facturacion.Domain.EntitiesEntityFramework
{
    public partial class FacturaDetalle
    {
        public int DetalleId { get; set; }
        public int FacturaId { get; set; }
        public string? Descripcion { get; set; }
        public int? Cantidad { get; set; }
        public decimal? PrecioUnitario { get; set; }

        public virtual FacturaCabecera Factura { get; set; } = null!;
    }
}
