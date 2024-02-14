
using CleaArchitecture.Infraestructure;
using CleanArchitecture.Application.Abstractions.Clock;
using CleanArchitecture.Application.Abstractions.Email;
using CleanArchitecture.Infraestructure;
using CleanArchitecture.Infrastructure.Clock;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure;

public static class DependencyInjection
{

    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration // es para leer la conf desde el archivo Appsettings.json--se reuiere package--dotnet add package Microsoft.Extensions.Configuration.Abstractions --version 7.0.0 
        )
    {

        services.AddTransient<IDateTimeProvider, DateTimeProvider>();  // cada servicio debe tener su interfaz e implementacion
        services.AddTransient<IEmailService, EmailService>();

        var connectionString = configuration.GetConnectionString("Database") 
             ?? throw new ArgumentNullException(nameof(configuration));  // si no existe esa configuracion en al archivo .json entonces se envia un error

        services.AddDbContext<ApplicationDbContext>(options => {
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();  //UseSnakeCaseNamingConvention = con el paquete Nameconventions entonces evitara usar mayusculas
        });


        return services;
    }

}