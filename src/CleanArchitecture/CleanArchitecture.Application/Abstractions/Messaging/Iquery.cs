using CleanArchitecture.Domain.Abstractions;
using MediatR;

namespace CleaArchitecture.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>   // esto es personalizado y es de tipo Result para que soporte todo tipo de datos--- generico
{  

}