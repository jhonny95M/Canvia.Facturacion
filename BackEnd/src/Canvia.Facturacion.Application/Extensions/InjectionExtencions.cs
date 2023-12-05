using Canvia.Facturacion.Application.Interfaces;
using Canvia.Facturacion.Application.Services;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Canvia.Facturacion.Application.Extensions;

public static class InjectionExtencions
{
    public static IServiceCollection AddInjectionApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration);
        services.AddFluentValidation(options =>
        {
            options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies().Where(c => !c.IsDynamic));
        });

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<IClienteApplication, ClienteApplication>();
        services.AddScoped<IFacturaApplication, FacturaApplication>();
        return services;
    }
}
