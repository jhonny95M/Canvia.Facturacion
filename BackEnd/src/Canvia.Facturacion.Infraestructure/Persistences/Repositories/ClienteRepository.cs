using Canvia.Facturacion.Domain.EntitiesAdoNet;
using Canvia.Facturacion.Infraestructure.Commons.Bases.Request;
using Canvia.Facturacion.Infraestructure.Commons.Bases.Response;
using Canvia.Facturacion.Infraestructure.Persistences.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Canvia.Facturacion.Infraestructure.Persistences.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly SqlConnection connection;
    private readonly SqlTransaction transaction;
    public ClienteRepository(SqlConnection connection,SqlTransaction transaction)
    {
        this.connection = connection;
        this.transaction = transaction;
    }
    public async Task ActualizarClienteAsync(Cliente cliente)
    {
        using (SqlCommand command = new SqlCommand("ActualizarCliente", connection, transaction))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ClienteID", cliente.ClienteID);
            command.Parameters.AddWithValue("@Nombre", cliente.Nombre);
            command.Parameters.AddWithValue("@Apellido", cliente.Apellido);
            command.Parameters.AddWithValue("@Email", cliente.Email);

            await command.ExecuteNonQueryAsync();
        }
    }

    public async Task EliminarClienteAsync(int clienteId)
    {
        using (SqlCommand command = new SqlCommand("DELETE FROM CLIENTE WHERE ClienteID = @ClienteID", connection, transaction))
        {
            command.Parameters.AddWithValue("@ClienteID", clienteId);
            await command.ExecuteNonQueryAsync();
        }
    }

    public async Task InsertarClienteAsync(Cliente cliente)
    {
        using (SqlCommand command = new SqlCommand("InsertarCliente; SELECT SCOPE_IDENTITY();", connection, transaction))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Nombre", cliente.Nombre);
            command.Parameters.AddWithValue("@Apellido", cliente.Apellido);
            command.Parameters.AddWithValue("@Email", cliente.Email);

            // Obtener el ID generado por la base de datos después de la inserción
            cliente.ClienteID = Convert.ToInt32(await command.ExecuteScalarAsync());
        }
    }

    public async Task<Cliente> ObtenerPorIdAsync(int clienteId)
    {
        Cliente? cliente = null;

        using (SqlCommand command = new SqlCommand("SELECT * FROM CLIENTE WHERE ClienteID = @ClienteID", connection, transaction))
        {
            command.Parameters.AddWithValue("@ClienteID", clienteId);

            using (SqlDataReader reader =await command.ExecuteReaderAsync())
            {
                if (reader.Read())
                {
                    cliente = new Cliente
                    {
                        ClienteID = Convert.ToInt32(reader["ClienteID"]),
                        Nombre = reader["Nombre"].ToString(),
                        Apellido = reader["Apellido"].ToString(),
                        Email = reader["Email"].ToString()
                    };
                }
            }
        }

        return cliente!;
    }

    public async Task<BaseEntityResponse<Cliente>> ObtenerTodosAsync(BaseFiltersRequest? filters = null)
    {
        List<Cliente> clientes = new List<Cliente>();

        using (SqlCommand command = new SqlCommand("SELECT * FROM CLIENTE", connection, transaction))
        {
            using (SqlDataReader reader =await command.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    clientes.Add(new Cliente
                    {
                        ClienteID = Convert.ToInt32(reader["ClienteID"]),
                        Nombre = reader["Nombre"].ToString(),
                        Apellido = reader["Apellido"].ToString(),
                        Email = reader["Email"].ToString()
                    });
                }
            }
        }

        return new BaseEntityResponse<Cliente>
        {
            Items = clientes,
            TotalRecords=1
        };
    }
}
