using CleaArchitecture.Application.Abstractions.Messaging;

namespace CleaArchitecture.Application.Alquileres.GetAlquiler;

public sealed record GetAlquilerQuery(Guid AlquilerId) : IQuery<AlquilerResponse>;  //necesito implementar la interface Iquery