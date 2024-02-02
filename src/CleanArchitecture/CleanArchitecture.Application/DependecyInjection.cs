using System.Runtime.Serialization;
using CleanArchitecture.Domain.Alquileres;
using Microsoft.Extensions.DependencyInjection;

namespace CleaArchitecture.Application;

public static class DependencyInjection
{  // Como es la encargada de registra los servicios entonces por eso es static

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });

        services.AddTransient<PrecioService>();
        return services;
    }
}