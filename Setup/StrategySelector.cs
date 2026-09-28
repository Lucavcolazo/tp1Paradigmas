using ElementalGame.AI;
using ElementalGame.Models;

namespace ElementalGame.Setup;

// Knows the available AI types: the player picks one or lets it be drawn at random.
public class StrategySelector
{
    // [Polymorphism] A single list of IStrategy holds three different types of AI,
    // and they are all treated the same way.
    // [Encapsulation] The list is private and exposed read-only.
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
