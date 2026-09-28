using ElementalGame.Models;

namespace ElementalGame.AI;

// [Abstraction] Interface: defines WHAT every AI must know how to do (choose a unit), not HOW.
// [Polymorphism] Every class that implements it can be used interchangeably by AIPlayer.
public interface IStrategy
{
    string Name { get; }
    string Description { get; }
    ElementalUnit Choose(IReadOnlyList<ElementalUnit> available, ElementalUnit? rival);
}
