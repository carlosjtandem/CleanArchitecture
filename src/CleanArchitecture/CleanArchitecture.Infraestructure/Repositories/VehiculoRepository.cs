using CleaArchitecture.Infraestructure;
using CleanArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Infraestructure.Repositories;

internal sealed class VehiculoRepository : Repository<Vehiculo>, IVehiculoRepository
{
    public VehiculoRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}