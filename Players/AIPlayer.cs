using ElementalGame.AI;
using ElementalGame.Models;

namespace ElementalGame.Players;

// [Inheritance] AIPlayer is a Player, just like HumanPlayer.
public class AIPlayer : Player
{
    // [Polymorphism] It depends on the IStrategy interface, not on a concrete AI: any strategy
    // (random, strategic, super) can be plugged in without changing this class (Strategy pattern).
    private readonly IStrategy strategy;

    public AIPlayer(IStrategy strategy, IEnumerable<ElementalUnit> units)
        : base(strategy.Name, units)
    {
        this.strategy = strategy;
    }

    // [Polymorphism] Override: the AI delegates the choice to its strategy.
    protected override ElementalUnit ChooseUnit(IReadOnlyList<ElementalUnit> available, ElementalUnit? rival)
    {
        return strategy.Choose(available, rival);
    }
}
