using CleaArchitecture.Application.Alquileres.GetAlquiler;
using CleaArchitecture.Application.Alquileres.ReservarAlquiler;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers.Alquileres;

[ApiController]
[Route("api/alquileres")]

public class AlquileresController : ControllerBase
{
    private readonly ISender _sender;

    public AlquileresController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAlquiler(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        var query = new GetAlquilerQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.IsSuccess ? Ok(resultado.Value) : NotFound();  //retorna un valor si el proceso de busqueda es exitoso, caso contrario devuelve un estatus not found(esto forma parte de Asp.Net core).
    }

    [HttpPost]
    public async Task<IActionResult> ReservarAlquiler(
        Guid id,
        AlquilerReservaRequest request,
        CancellationToken cancellationToken)
    {
        var command = new ReservarAlquilerCommand(request.VehiculoId, request.UserId, request.StartDate, request.EndDate); //esto es el command porque es post
        var resultado = await _sender.Send(command, cancellationToken); //enviamos el command a su handler

        if (resultado.IsFailure)
        {
            return BadRequest(resultado.Error);
        }

        return CreatedAtAction(nameof(GetAlquiler), new { id = resultado.Value });  //LLama al metodo de arriba de busqueda un alquiler por id
    }
}