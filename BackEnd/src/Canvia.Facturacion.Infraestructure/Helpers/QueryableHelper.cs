using Canvia.Facturacion.Infraestructure.Commons.Bases.Request;

namespace Canvia.Facturacion.Infraestructure.Helpers;

public static class QueryableHelper
{
    public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, BasePaginationRequest paginationRequest) =>
        queryable.Skip((paginationRequest.NumPage - 1) * paginationRequest.Records).Take(paginationRequest.Records);
}
