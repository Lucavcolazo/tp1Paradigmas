using ElementalGame.Models;

namespace ElementalGame.AI;

// Strategy + efficiency: if some unit can finish off the rival in one hit, it uses the one that
// does it with the least advantage and energy, saving the best ones for later.
// If none can finish it off, it behaves like the strategic AI.
// [Inheritance] SuperAI is a StrategicAI "with something extra": it reuses the table and
// DamageDealt() without duplicating code.
public class SuperAI : StrategicAI
{
    // [Polymorphism] Overrides the name and description of the base class.
    public override string Name => "Super IA";
    public override string Description => "Ventaja de tipo + te remata con lo justo";

    public SuperAI(EffectivenessTable table) : base(table)
    {
    }

    // [Polymorphism] Overrides Choose(), and when it cannot finish off the rival it falls back
    // to the parent's behavior with base.Choose().
    public override ElementalUnit Choose(IReadOnlyList<ElementalUnit> available, ElementalUnit? rival)
    {
        var finisher = rival is null
            ? null
            : available
                .Where(u => DamageDealt(u, rival) >= rival.Energy)
                .OrderBy(u => DamageDealt(u, rival))
                .ThenBy(u => u.Energy)
                .FirstOrDefault();

        return finisher ?? base.Choose(available, rival);
    }
}
