using tp1Paradigmas.AI;
using tp1Paradigmas.Models;

namespace tp1Paradigmas.Setup;

// Conoce los tipos de IA disponibles: el jugador elige uno o deja que se sortee.
public class StrategySelector
{
    // [Polimorfismo] Una sola lista de IStrategy guarda tres tipos distintos de IA,
    // y todas se tratan de la misma forma.
    // [Encapsulamiento] La lista es privada y se expone de solo lectura.
    private readonly List<IStrategy> strategies;

    public IReadOnlyList<IStrategy> Strategies => strategies.AsReadOnly();

    public StrategySelector(EffectivenessTable table)
    {
        strategies = new List<IStrategy>
        {
            new RandomAI(),
            new StrategicAI(table),
            new SuperAI(table)
        };
    }

    public IStrategy PickRandom()
    {
        return strategies[Random.Shared.Next(strategies.Count)];
    }
}
