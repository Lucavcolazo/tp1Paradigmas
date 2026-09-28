using ElementalGame.Models;

namespace ElementalGame.AI;

// Picks the unit with the best balance between damage dealt and damage taken against the rival.
// [Polymorphism] Implements IStrategy.
public class StrategicAI : IStrategy
{
    // [Encapsulation] protected: hidden from the outside, but accessible to subclasses (SuperAI).
    protected EffectivenessTable Table { get; }

    // [Polymorphism] virtual members: subclasses can redefine them.
    public virtual string Name => "IA Estratégica";
    public virtual string Description => "Busca la ventaja de tipo contra tu elemento";

    public StrategicAI(EffectivenessTable table)
    {
        Table = table;
    }

    public virtual ElementalUnit Choose(IReadOnlyList<ElementalUnit> available, ElementalUnit? rival)
    {
        return rival is null
            ? available.MaxBy(u => u.Energy)!
            : available
                .OrderByDescending(u => Advantage(u, rival))
                .ThenByDescending(u => u.Energy)
                .First();
    }

    // [Encapsulation] protected helpers: SuperAI reuses them, but they are not part of the public API.
    protected double DamageDealt(ElementalUnit own, ElementalUnit rival) =>
        Table.GetDamage(own.Type, rival.Type);

    protected double DamageTaken(ElementalUnit own, ElementalUnit rival) =>
        Table.GetDamage(rival.Type, own.Type);

    private double Advantage(ElementalUnit own, ElementalUnit rival) =>
        DamageDealt(own, rival) - DamageTaken(own, rival);
}
