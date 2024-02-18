using CleanArchitecture.Application.Vehiculos.SearchVehiculos;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace CleanArchitecture.Api.Controllers.Vehiculos;


[ApiController]
[Route("api/vehiculos")]
public class VehiculosController : ControllerBase// hereda de controller BASe -- Asp net core MVC
{
    private readonly ISender _sender;  //Interface que pertenece a MeadiaTR

    public VehiculosController(ISender sender)  //inyectamos dentro de la clase y se inyecta como un obj
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> SearchVEhiculos(
        DateOnly starDate,
        DateOnly endDate,
        CancellationToken cancellationToken
    )
    {
        var query = new SearchVehiculosQuery(starDate, endDate);
        var resultados = await _sender.Send(query, cancellationToken);
        return Ok(resultados.Value);
    }

    
}