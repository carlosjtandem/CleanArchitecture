using System.Data;
using Dapper;

namespace CleanArchitecture.Infraestructure.Data;

internal sealed class DateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly>
{
    public override DateOnly Parse(object value) => DateOnly.FromDateTime((DateTime)value);  // esto es programacion funcional cuando puedes resolver en unsa sola linea


    public override void SetValue(IDbDataParameter parameter, DateOnly value)
    {
        parameter.DbType = DbType.Date;
        parameter.Value = value;
    }
}