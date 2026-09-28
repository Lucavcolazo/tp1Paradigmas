using ElementalGame.AI;
using ElementalGame.Models;

namespace ElementalGame.Players;

// [Herencia] AIPlayer es un Player, igual que HumanPlayer.
public class AIPlayer : Player
{
    // [Polimorfismo] Depende de la interfaz IStrategy, no de una IA concreta: cualquier estrategia
    // (aleatoria, estratégica, súper) se puede enchufar sin cambiar esta clase (patrón Strategy).
    private readonly IStrategy strategy;

    public AIPlayer(IStrategy strategy, IEnumerable<ElementalUnit> units)
        : base(strategy.Name, units)
    {
        this.strategy = strategy;
    }

    // [Polimorfismo] Override: la IA delega la elección en su estrategia.
    protected override ElementalUnit ChooseUnit(IReadOnlyList<ElementalUnit> available, ElementalUnit? rival)
    {
        return strategy.Choose(available, rival);
    }
}
