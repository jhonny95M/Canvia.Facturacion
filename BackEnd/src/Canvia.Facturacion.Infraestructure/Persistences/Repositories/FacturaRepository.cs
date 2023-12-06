using Canvia.Facturacion.Domain.EntitiesAdoNet;
using Canvia.Facturacion.Infraestructure.Commons.Bases.Request;
using Canvia.Facturacion.Infraestructure.Commons.Bases.Response;
using Canvia.Facturacion.Infraestructure.Context;
using Canvia.Facturacion.Infraestructure.Persistences.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;

namespace Canvia.Facturacion.Infraestructure.Persistences.Repositories;

public class FacturaRepository : AbstractGenericRepository<Domain.EntitiesEntityFramework.FacturaCabecera>, IFacturaRepository
{
    private readonly SqlConnection connection;
    private readonly SqlTransaction transaction;
    public FacturaRepository(SqlConnection connection, SqlTransaction transaction, PruebaTecnicaCanviaContext context) :base(context)
    {
        this.connection = connection;
        this.transaction = transaction;
    }
    public async Task AnularFacturaAsync(int facturaId)
    {

        // Eliminar detalles primero
        using (SqlCommand command = new SqlCommand("AnularFactura", connection, transaction))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@FacturaID", facturaId);
            await command.ExecuteNonQueryAsync();
        }
    }

    public async Task<bool> EditarFacturaAsync(FacturaCabeceraAdoNet factura)
    {
        using (SqlCommand command = new SqlCommand("ActualizarFacturaCabecera", connection, transaction))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ClienteID", factura.ClienteID);
            command.Parameters.AddWithValue("@FechaEmision", factura.FechaEmision);
            command.Parameters.AddWithValue("@Total", factura.Total);
            command.Parameters.AddWithValue("@Descripcion", factura.Descripcion);
            command.Parameters.AddWithValue("@FacturaID", factura.FacturaID);
            var rowsAffected = await command.ExecuteNonQueryAsync();
            return true;
        }
    }

    public async Task<bool> EliminarFacturaDetalleByIdFacturaAsync(int idFactura)
    {
        using (SqlCommand command = new SqlCommand("EliminarDetallePorFacturaID", connection, transaction))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@FacturaID", idFactura);
            await command.ExecuteNonQueryAsync();
            return true;
        }
    }

    public async Task InsertarDetalleFacturaAsync(FacturaDetalle detalle)
    {
        using (SqlCommand command = new SqlCommand("InsertarDetalleFactura", connection, transaction))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@DetalleID", detalle.DetalleID);
            command.Parameters.AddWithValue("@FacturaID", detalle.FacturaID);
            command.Parameters.AddWithValue("@Descripcion", detalle.Descripcion);
            command.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
            command.Parameters.AddWithValue("@PrecioUnitario", detalle.PrecioUnitario);

            await command.ExecuteNonQueryAsync();
        }
    }

    public async Task<bool> InsertarFacturaAsync(FacturaCabeceraAdoNet factura)
    {
        using (SqlCommand command = new SqlCommand("InsertarFactura", connection, transaction))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ClienteID", factura.ClienteID);
            command.Parameters.AddWithValue("@FechaEmision", factura.FechaEmision);
            command.Parameters.AddWithValue("@Descripcion", factura.Descripcion);
            command.Parameters.AddWithValue("@Total", factura.Total);

            // Obtener el ID generado por la base de datos después de la inserción
            factura.FacturaID = Convert.ToInt32(await command.ExecuteScalarAsync());
            return factura.FacturaID > 0;
        }
    }

    public async Task<IEnumerable<FacturaDetalle>> ObtenerDetallePorIdAsync(int facturaId)
    {
        List<FacturaDetalle> facturas = new List<FacturaDetalle>();

        using (SqlCommand command = new SqlCommand("SELECT * FROM FACTURA_DETALLE WHERE FacturaID = @FacturaID", connection, transaction))
        {
            command.Parameters.AddWithValue("@FacturaID", facturaId);

            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    facturas.Add(new FacturaDetalle
                    {
                        DetalleID = Convert.ToInt32(reader["DetalleID"]),
                        FacturaID = Convert.ToInt32(reader["FacturaID"]),
                        Descripcion = reader["Descripcion"] as string,
                        Cantidad = Convert.ToInt32(reader["Cantidad"]),
                        PrecioUnitario = Convert.ToDecimal(reader["PrecioUnitario"])
                    });
                }
            }
        }

        return facturas;
    }

    public async Task<FacturaCabeceraAdoNet> ObtenerPorIdAsync(int facturaId)
    {
        FacturaCabeceraAdoNet? factura = null;

        using (SqlCommand command = new SqlCommand("SELECT * FROM FACTURA_CABECERA WHERE FacturaID = @FacturaID", connection, transaction))
        {
            command.Parameters.AddWithValue("@FacturaID", facturaId);

            using (SqlDataReader reader =await command.ExecuteReaderAsync())
            {
                if (reader.Read())
                {
                    factura = new FacturaCabeceraAdoNet
                    {
                        FacturaID = Convert.ToInt32(reader["FacturaID"]),
                        ClienteID = Convert.ToInt32(reader["ClienteID"]),
                        FechaEmision = Convert.ToDateTime(reader["FechaEmision"]),
                        Total = Convert.ToDecimal(reader["Total"]),
                        Estado = Convert.ToByte(reader["Estado"]),
                        Descripcion = reader["Descripcion"] as string
                    };
                }
            }
        }

        return factura!;
    }

    public async Task<BaseEntityResponse<Domain.EntitiesEntityFramework.FacturaCabecera>> ObtenerTodasAsync(BaseFiltersRequest filters )
    {
        var response = new BaseEntityResponse<Domain.EntitiesEntityFramework.FacturaCabecera>();
        
        var facturas = GetEntityQuery(includes: new Expression<Func<Domain.EntitiesEntityFramework.FacturaCabecera, object>>[]{f => f.Cliente!});
        if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
        {
            switch (filters.NumFilter)
            {
                case 1:
                    facturas = facturas.Where(c => c.Descripcion!.Contains(filters.TextFilter));
                    break;
                case 2:
                    //categories = categories.Where(c => c.Description!.Contains(filters.TextFilter));
                    break;
            }
        }
        if (filters.StateFilter is not null)
            facturas = facturas.Where(c => c.Estado==filters.StateFilter);
        if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
            facturas = facturas.Where(c => c.FechaEmision >= Convert.ToDateTime(filters.StartDate) && c.FechaEmision <= Convert.ToDateTime(filters.EndDate).AddDays(1));
        if (filters.Sort is null) filters.Sort = nameof(FacturaCabeceraAdoNet.FacturaID);
        response.TotalRecords = await facturas.CountAsync();
        var query = Ordering(filters, facturas, !(bool)filters.Download!);        
        response.Items = await query.ToListAsync();
        return response;
    }
}
