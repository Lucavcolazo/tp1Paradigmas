using ElementalGame.Models;

namespace ElementalGame.AI;

// [Abstracción] Interfaz: define QUÉ tiene que saber hacer toda IA (elegir una unidad), no CÓMO.
// [Polimorfismo] Cualquier clase que la implemente puede ser usada indistintamente por AIPlayer.
public interface IStrategy
{
    string Name { get; }
    string Description { get; }
    ElementalUnit Choose(IReadOnlyList<ElementalUnit> available, ElementalUnit? rival);
}
