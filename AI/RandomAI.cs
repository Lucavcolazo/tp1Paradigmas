using ElementalGame.Models;

namespace ElementalGame.AI;

// Picks at random, without looking at the rival.
// [Polymorphism] Implements IStrategy directly: it shares no logic with the other AIs,
// so it doesn't inherit from any of them.
public class RandomAI : IStrategy
{
    public string Name => "IA Aleatoria";
    public string Description => "Elige al azar, sin mirar tu elemento";

    public ElementalUnit Choose(IReadOnlyList<ElementalUnit> available, ElementalUnit? rival)
    {
        return available[Random.Shared.Next(available.Count)];
    }
}
