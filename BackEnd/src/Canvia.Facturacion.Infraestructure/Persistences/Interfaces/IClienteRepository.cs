using Canvia.Facturacion.Domain.EntitiesAdoNet;
using Canvia.Facturacion.Infraestructure.Commons.Bases.Request;

namespace Canvia.Facturacion.Infraestructure.Persistences.Interfaces;

public interface IClienteRepository
{
    Task<IEnumerable<Cliente>> ObtenerTodosAsync(BaseFiltersRequest? filters=null);

    Task<Cliente> ObtenerPorIdAsync(int clienteId);

    Task InsertarClienteAsync(Cliente cliente);

    Task ActualizarClienteAsync(Cliente cliente);

    Task EliminarClienteAsync(int clienteId);
}
