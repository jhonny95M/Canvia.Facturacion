using Canvia.Facturacion.Infraestructure.Context;
using Canvia.Facturacion.Infraestructure.Persistences.Interfaces;
using Canvia.Facturacion.Infraestructure.Persistences.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Canvia.Facturacion.Infraestructure.Extensions;

public static class InjectionExtensions
{
    public static IServiceCollection AddInjectionInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("FacturacionConnection");
        var assembly = typeof(PruebaTecnicaCanviaContext).Assembly.FullName;
        services.AddDbContext<PruebaTecnicaCanviaContext>(options =>
            options.UseSqlServer(connectionString, b => b.MigrationsAssembly(assembly)), ServiceLifetime.Transient
        );
        services.AddTransient<IUnitOfWork, UnitOfWork>((s)=> new UnitOfWork(connectionString,s.GetRequiredService<PruebaTecnicaCanviaContext>()));
        //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        return services;
    }
}
