using CleaArchitecture.Application.Abstractions.Messaging;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Abstractions.Behaviors;


public class LogginBehavior<TRequest, Tresponse>  // Clase generica que parseara dos clases REquest y response
: IPipelineBehavior<TRequest, Tresponse>
where TRequest : IBaseCommand  //solo va a loggear los request de tipo command
{

    private readonly ILogger<TRequest> _logger;  //LOG de Microsoft

    public LogginBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<Tresponse> Handle(
        TRequest request,
        RequestHandlerDelegate<Tresponse> next,
        CancellationToken cancellationToken
        )
    {
        var name = request.GetType().Name; //Retorna el nombre de la clase o del commando (solo funcionara para commands)

        try
        {
            _logger.LogInformation($"Ejecutando el command request: {name}");
            var result = await next();
            _logger.LogInformation($"El commando {name} se ejecutó correctamente");

            return result;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"El comando {name} tiene errores");
            throw;
        }
    }
}