using CleaArchitecture.Infraestructure;
using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Vehiculos;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infraestructure.Repositories;

internal sealed class AlquilerRepository : Repository<Alquiler>, IAlquilerRepository
{
    //arreglo de estados
    private static readonly AlquilerStatus[] ActiveAlquilerStatuses = {
        AlquilerStatus.Reservado,
        AlquilerStatus.Confirmado,
        AlquilerStatus.Completado
    };

    public AlquilerRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> IsOverlappingAsync(
        Vehiculo vehiculo,
        DateRange duracion,
        CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Alquiler>()  //aqui se devuelve un status para ver si puedo alquilar el caroo
        .AnyAsync(
            alquiler =>
            alquiler.VehiculoId == vehiculo.Id &&
            alquiler.Duracion!.Inicio <= duracion.Fin &&
            alquiler.Duracion.Fin >= duracion.Inicio &&
            ActiveAlquilerStatuses.Contains(alquiler.Status),
            cancellationToken
        );
    }
}
