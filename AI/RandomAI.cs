using tp1Paradigmas.Models;

namespace tp1Paradigmas.AI;

public class RandomAI : IStrategy
{
    public string Name => "IA Aleatoria";
    public string Description => "Elige al azar, sin mirar tu elemento";

    public ElementalUnit Choose(IReadOnlyList<ElementalUnit> available, ElementalUnit? rival)
    {
        return available[Random.Shared.Next(available.Count)];
    }
}
