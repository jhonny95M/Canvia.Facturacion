using Canvia.Facturacion.Domain.EntitiesAdoNet;
using Canvia.Facturacion.Domain.EntitiesEntityFramework;
using Canvia.Facturacion.Infraestructure.Commons.Bases.Request;
using Canvia.Facturacion.Infraestructure.Commons.Bases.Response;
using Canvia.Facturacion.Infraestructure.Dtos;
using FacturaDetalle = Canvia.Facturacion.Domain.EntitiesAdoNet.FacturaDetalle;

namespace Canvia.Facturacion.Infraestructure.Persistences.Interfaces;

public interface IFacturaRepository
{    
    Task<BaseEntityResponse<FacturaCabeceraEntityDto>> ObtenerTodasAsync(BaseFiltersRequest filters);

    Task<FacturaCabeceraAdoNet> ObtenerPorIdAsync(int facturaId);
    Task<IEnumerable<FacturaDetalle>> ObtenerDetallePorIdAsync(int facturaId);

    Task<bool> InsertarFacturaAsync(FacturaCabeceraAdoNet factura);
    Task<bool> EditarFacturaAsync(FacturaCabeceraAdoNet factura);
    Task<bool> EliminarFacturaDetalleByIdFacturaAsync(int idFactura);

    Task InsertarDetalleFacturaAsync(FacturaDetalle detalle);

    Task AnularFacturaAsync(int facturaId);
}
