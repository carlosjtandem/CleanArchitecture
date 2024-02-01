using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Alquileres;

public sealed record AlquilerReservadoDomainEvent(Guid AlquilerId) : IDomainsEvent;