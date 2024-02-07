using CleanArchitecture.Application.Abstractions.Data;
using CleaArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Alquileres;
using Dapper;

namespace CleanArchitecture.Application.Vehiculos.SearchVehiculos;

internal sealed class SearchVehiculosQueryHandler
 : IQueryHandler<SearchVehiculosQuery, IReadOnlyList<VehiculoResponse>>
{

    private static readonly int[] ActiveAlquilerStatuses =  // esto es una colleccion de enteros
    {
        (int)AlquilerStatus.Reservado,  //le añadimos (int) al inicio para parsearlo a entero
        (int)AlquilerStatus.Confirmado,
        (int)AlquilerStatus.Completado
    };


    private readonly ISqlConnectionFactory _sqlConectionFactory;

    public SearchVehiculosQueryHandler(ISqlConnectionFactory sqlConectionFactory)
    {
        _sqlConectionFactory = sqlConectionFactory;
    }

    public async Task<Result<IReadOnlyList<VehiculoResponse>>> Handle(
        SearchVehiculosQuery request,
        CancellationToken cancellationToken
    )
    {
        if (request.fechaInicio > request.fechaFin)  //En esta caso no deberia retornar nada la consulta, en ese caso retorna una lista en blanco
        {
            return new List<VehiculoResponse>();
        }

        using var connection = _sqlConectionFactory.CreateConnection();
        // Query complejo que divide en dos clases
        //El query en vez de traer todo lineal se va a separa data para vehiculo y datos para direccion en clases diferentes.
        const string sql = """
             SELECT
                a.id as Id,
                a.modelo as Modelo,
                a.vin as Vin,
                a.precio_monto as Precio,
                a.precio_tipo_moneda as TipoMoneda,
                a.direccion_pais as Pais,
                a.direccion_departamento as Departamento,
                a.direccion_provincia as Provincia,
                a.direccion_ciudad as Ciudad,
                a.direccion_calle as Calle
             FROM vehiculos AS a
             WHERE NOT EXISTS
             (
                    SELECT 1 
                    FROM alquileres AS b
                    WHERE 
                        b.vehiculo_id = a.id
                        b.duracion_inicio <= @EndDate AND
                        b.duracion_final  >= @StartDate AND
                        b.status = ANY(@ActiveAlquilerStatuses)
             )       
        """;


        var vehiculos = await connection
            .QueryAsync<VehiculoResponse, DireccionResponse, VehiculoResponse>  // quiero que me devuelva un conjunto de 3 resultados deonde los dos primeros grupos (VehiculoResponse, DireccionResponse) van a estar incluidos en el padre (VehiculoResponse) que es el 3er parametro
            (
                sql,
                (vehiculo, direccion) =>  // este EXPRESSION permite  devolver dos objetos
                {
                    vehiculo.Direccion = direccion;
                    return vehiculo;
                },
                new //se debe definir los parametros que debe tener el query
                {
                    StartDate = request.fechaInicio,
                    EndDate = request.fechaFin,
                    ActiveAlquilerStatuses
                },
                splitOn: "Pais"
            );

        return vehiculos.ToList();
    }
}