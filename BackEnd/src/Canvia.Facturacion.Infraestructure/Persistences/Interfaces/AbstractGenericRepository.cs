using Canvia.Facturacion.Infraestructure.Commons.Bases.Request;
using Canvia.Facturacion.Infraestructure.Context;
using Canvia.Facturacion.Infraestructure.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;

namespace Canvia.Facturacion.Infraestructure.Persistences.Interfaces;

public abstract class AbstractGenericRepository<T>where T : class
{
    private readonly PruebaTecnicaCanviaContext context;
    private readonly DbSet<T> entity;

    public AbstractGenericRepository(PruebaTecnicaCanviaContext context)
    {
        this.context = context;
        this.entity = this.context.Set<T>();
    }
    public IQueryable<T> GetEntityQuery(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = entity;
        if (filter != null) query = query.Where(filter);
        foreach (var includeExpression in includes)
           query = query.Include(includeExpression);
        return query;
    }
    public IQueryable<TResult> Ordering<TResult>(BasePaginationRequest paginationRequest, IQueryable<TResult> queryable, bool pagination = false) where TResult : class
    {
        IQueryable<TResult> queryDto = paginationRequest.Order == "desc" ? queryable.OrderBy($"{paginationRequest.Sort} descending") : queryable.OrderBy($"{paginationRequest.Sort} ascending");
        if (pagination) queryDto = queryDto.Paginate(paginationRequest);

        return queryDto;
    }
}

