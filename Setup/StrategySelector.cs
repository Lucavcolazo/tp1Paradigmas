using tp1Paradigmas.AI;
using tp1Paradigmas.Models;

namespace tp1Paradigmas.Setup;

public class StrategySelector
{
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
