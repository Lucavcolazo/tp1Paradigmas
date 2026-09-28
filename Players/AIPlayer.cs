using tp1Paradigmas.AI;
using tp1Paradigmas.Models;

namespace tp1Paradigmas.Players;

public class AIPlayer : Player
{
    private readonly IStrategy strategy;

    public AIPlayer(IStrategy strategy, IEnumerable<ElementalUnit> units)
        : base(strategy.Name, units)
    {
        this.strategy = strategy;
    }

    protected override ElementalUnit ChooseUnit(IReadOnlyList<ElementalUnit> available, ElementalUnit? rival)
    {
        return strategy.Choose(available, rival);
    }
}
