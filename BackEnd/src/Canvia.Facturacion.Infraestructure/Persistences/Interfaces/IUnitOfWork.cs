namespace Canvia.Facturacion.Infraestructure.Persistences.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IClienteRepository Clientes { get; }
    IFacturaRepository Facturas { get; }

    void Commit();
    void Rollback();
}
