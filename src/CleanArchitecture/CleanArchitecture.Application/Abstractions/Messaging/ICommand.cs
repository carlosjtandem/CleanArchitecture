using CleanArchitecture.Domain.Abstractions;
using MediatR;

namespace CleaArchitecture.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>, IBaseCommand
{

}

//cuando se requiera retornar valor despues de una transaccion (no es lo recomendable pero se puede hacer)
public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
{

}

public interface IBaseCommand
{

}