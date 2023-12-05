using Canvia.Facturacion.Infraestructure.Context;
using Canvia.Facturacion.Infraestructure.Persistences.Interfaces;
using Microsoft.Data.SqlClient;

namespace Canvia.Facturacion.Infraestructure.Persistences.Repositories
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;
        private SqlTransaction _transaction;
        private readonly PruebaTecnicaCanviaContext context;

        public UnitOfWork(string connectionString,PruebaTecnicaCanviaContext context)
        {
            this.context = context;
            _connectionString = connectionString;
            _connection = new SqlConnection(_connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        private IClienteRepository? _clientes;
        public IClienteRepository Clientes => _clientes ??= new ClienteRepository(_connection, _transaction);

        private IFacturaRepository? _facturas;
        public IFacturaRepository Facturas => _facturas ??= new FacturaRepository(_connection, _transaction,context);

        public void Commit()=>
                _transaction?.Commit();
        public void Rollback()=>
                _transaction?.Rollback();

        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Close();
        }
    }
}
