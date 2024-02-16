using CleaArchitecture.Infraestructure;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infraestructure.Repositories;

internal abstract class Repository<T>
where T : Entity
{
    // indicarle que inyecte a Dbcontext

    protected readonly ApplicationDbContext DbContext;

    protected Repository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<T?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default
    )
    {
        return await DbContext.Set<T>()
        .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
    }

    //Para agregar un elemento a la persistencia
    public void Add(T entity)
    {
        DbContext.Add(entity);
    }

}