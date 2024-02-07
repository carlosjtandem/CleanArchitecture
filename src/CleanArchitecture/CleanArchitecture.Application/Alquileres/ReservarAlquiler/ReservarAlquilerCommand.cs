using CleaArchitecture.Application.Abstractions.Messaging;

namespace CleaArchitecture.Application.Alquileres.ReservarAlquiler;
public record ReservarAlquilerCommand(  // SE retorna el ID del registro creado.
    Guid VehiculoId,
    Guid UserId,
    DateOnly FechaInicio,
    DateOnly FechaFin
) : ICommand<Guid>;