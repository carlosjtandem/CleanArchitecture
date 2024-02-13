using System.ComponentModel.DataAnnotations;
using CleaArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Application.Exceptions;
using FluentValidation;
using MediatR;

namespace CleanArchitecture.Application.Abstractions.Behaviors;

public class ValidationBehavior<TRequest, TResponse>
: IPipelineBehavior<TRequest, TResponse>
where TRequest : IBaseCommand
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;  //Podemos hacer multiples validaciones al objeto request que envia el cliente

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)  //CTOR
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())// verificar si tengo alguna verificacion por algo que esta mal escrito (existe alguna validacion que ha faltado?)
        {
            return await next();
        }

        //Si hay error entonces creamos un validation context
        var context = new ValidationContext<TRequest>(request);

        var validationErrors = _validators
          .Select(validators => validators.Validate(context))
          .Where(validationResult => validationResult.Errors.Any())//Solo quiero las validaciones de tipo error
          .SelectMany(validationResult => validationResult.Errors)  //Ahora con la lista la voy a proyectar -- eso significa que en la siguiente linea lo voy a llevar a otro tipo de obj
          .Select(validationFailure => new ValidationError(   //Los voy a llevar a una nueva clase(obj) llamada ValidateError -- esto es una proyecccion
              validationFailure.PropertyName,
              validationFailure.ErrorMessage
          )).ToList();

          //puede que la validacion retorne null then
          if(validationErrors.Any())
          {
            throw new Exceptions.ValidationException(validationErrors);  // Esto es con un clase para manejarlo de manera personalizada
          }

          return await next();
    }
}