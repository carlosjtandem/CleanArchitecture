using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleaArchitecture.Infraestructure;

public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    private readonly IPublisher _publisher;
    public ApplicationDbContext(DbContextOptions options, IPublisher publisher) : base(options)
    {
        _publisher = publisher;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // para que se apliquen las configuracion del model builder
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);  //el assembly yiene el DbContext, esto significa que cuando el modelo este configurado este va a escanear este asembly encontrando cada configuracion de las entidades.. eso lo lee del directorio Configurations.
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            await PublishDomainEventsAsync();

            return result;
        }
        catch (DbUpdateConcurrencyException ex)  //Cuando ocurre una violacion de las reglas al insertar en la DB
        {
            throw new ConcurrencyException("La excepcion por concurrencia se dispario", ex);
            throw;
        }


    }

    private async Task PublishDomainEventsAsync()
    {
        var domainEvents = ChangeTracker
        .Entries<Entity>()
        .Select(entry => entry.Entity)
        .SelectMany(entity =>
        {
            var domainEvents = entity.GetDomainsEvents();
            entity.ClearDomainEvents();
            return domainEvents;
        }).ToList();

        foreach (var damainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvents);
        }
    }
}