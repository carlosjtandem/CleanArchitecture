using System.Runtime.Serialization;
using CleanArchitecture.Application.Abstractions.Behaviors;
using CleanArchitecture.Domain.Alquileres;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Application;

public static class DependencyInjection
{  // Como es la encargada de registra los servicios entonces por eso es static

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            //registro del servicio para logging
            configuration.AddOpenBehavior(typeof(LogginBehavior<,>));  //registro la clase generica con dos parametros, para eso typeof <,>+
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        
        services.AddTransient<PrecioService>();
        return services;
    }
}