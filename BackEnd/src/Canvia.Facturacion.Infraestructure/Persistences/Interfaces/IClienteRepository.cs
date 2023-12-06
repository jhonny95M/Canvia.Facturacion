using Canvia.Facturacion.Domain.EntitiesAdoNet;
using Canvia.Facturacion.Infraestructure.Commons.Bases.Request;
using Canvia.Facturacion.Infraestructure.Commons.Bases.Response;

namespace Canvia.Facturacion.Infraestructure.Persistences.Interfaces;

public interface IClienteRepository
{
    Task<BaseEntityResponse<Cliente>> ObtenerTodosAsync(BaseFiltersRequest? filters=null);

    Task<Cliente> ObtenerPorIdAsync(int clienteId);

    Task InsertarClienteAsync(Cliente cliente);

    Task ActualizarClienteAsync(Cliente cliente);

    Task EliminarClienteAsync(int clienteId);
}
