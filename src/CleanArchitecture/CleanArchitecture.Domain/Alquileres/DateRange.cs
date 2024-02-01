using CleanArchitecture.Domain.Alquileres;

public sealed record DateRange
{
    private DateRange()
    {

    }

    //creamos una fecha pero no tendra horas ya que no nos interesa la hora de alquiler.
    public DateOnly Inicio { get; init; }
    public DateOnly Fin { get; init; }

    public int CantidadDias => Fin.DayNumber - Inicio.DayNumber;

    public static DateRange Create(DateOnly inicio, DateOnly fin)
    {
        if (inicio > fin)
        {
            throw new ApplicationException("LA fecha final es anterior a la fecha de inicio");
        }
        return new DateRange
        {
            Inicio = inicio,
            Fin = fin
        };
    }

}