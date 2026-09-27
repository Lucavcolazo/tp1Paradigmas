using JuegoElementos.Modelo;

namespace JuegoElementos.IA;

// Sortea que tipo de IA le toca al jugador.
public class SelectorDeEstrategia
{
    private readonly List<Func<IEstrategiaIA>> estrategias;

    public SelectorDeEstrategia(TablaEfectividad tabla)
    {
        estrategias = new List<Func<IEstrategiaIA>>
        {
            () => new IAAleatoria(),
            () => new IAEstrategica(tabla),
            () => new SuperIA(tabla)
        };
    }

    public IEstrategiaIA ElegirAlAzar()
    {
        return estrategias[Random.Shared.Next(estrategias.Count)]();
    }
}
