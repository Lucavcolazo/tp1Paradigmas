using JuegoElementos.IA;
using JuegoElementos.Modelo;

namespace JuegoElementos.Jugadores;

public class JugadorIA : Jugador
{
    private readonly IEstrategiaIA estrategia;

    public JugadorIA(IEstrategiaIA estrategia, IEnumerable<UnidadElemental> unidades)
        : base($"IA ({estrategia.Nombre})", unidades)
    {
        this.estrategia = estrategia;
    }

    protected override UnidadElemental ElegirUnidad(IReadOnlyList<UnidadElemental> disponibles, UnidadElemental? rival)
    {
        var elegida = estrategia.Elegir(disponibles, rival);
        Console.WriteLine($"{Nombre} elige a {elegida}");
        return elegida;
    }
}
