using tp1Paradigmas.Models;

namespace tp1Paradigmas.AI;

// Elige al azar, sin mirar al rival.
// [Polimorfismo] Implementa IStrategy directamente: no comparte lógica con las otras IAs,
// por eso no hereda de ninguna.
public class RandomAI : IStrategy
{
    public string Name => "IA Aleatoria";
    public string Description => "Elige al azar, sin mirar tu elemento";

    public ElementalUnit Choose(IReadOnlyList<ElementalUnit> available, ElementalUnit? rival)
    {
        return available[Random.Shared.Next(available.Count)];
    }
}
