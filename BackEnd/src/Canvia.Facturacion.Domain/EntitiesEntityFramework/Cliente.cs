using System;
using System.Collections.Generic;

namespace Canvia.Facturacion.Domain.EntitiesEntityFramework
{
    public partial class Cliente
    {
        public Cliente()
        {
            FacturaCabeceras = new HashSet<FacturaCabecera>();
        }

        public int ClienteId { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<FacturaCabecera> FacturaCabeceras { get; set; }
    }
}
