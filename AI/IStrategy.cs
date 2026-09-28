using tp1Paradigmas.Models;

namespace tp1Paradigmas.AI;

public interface IStrategy
{
    string Name { get; }
    string Description { get; }
    ElementalUnit Choose(IReadOnlyList<ElementalUnit> available, ElementalUnit? rival);
}
