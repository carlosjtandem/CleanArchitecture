using CleaArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Domain.Abstractions;
using Dapper;

namespace CleaArchitecture.Application.Alquileres.GetAlquiler;

//Porque es internal ?? Rta. El query handler no se debe exponer hacia componentes externos, quien se encarga de ser expuesto es solo el obj Query (GetAlquilerQuery en GetAlquilerQuery.cs)
internal sealed class GetAlquilerQueryHandler : IQueryHandler<GetAlquilerQuery, AlquilerResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetAlquilerQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<AlquilerResponse>> Handle(
        GetAlquilerQuery request,
        CancellationToken cancellationToken
        )
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        var alquiler = await connection.QueryFirstOrDefaultAsync<AlquilerResponse>(  //Esto es para alquileres por alquilerId
          sql,
          new
          {
              request.AlquilerId
          }
         );

         return alquiler!;
    }
}