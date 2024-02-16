using CleanArchitecture.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CleaArchitecture.Infraestructure;

public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // para que se apliquen las configuracion del model builder
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);  //el assembly yiene el DbContext, esto significa que cuando el modelo este configurado este va a escanear este asembly encontrando cada configuracion de las entidades.. eso lo lee del directorio Configurations.
        base.OnModelCreating(modelBuilder);
    }
}