using tp1Paradigmas.Models;

namespace tp1Paradigmas.AI;

public class SuperAI : StrategicAI
{
    public override string Name => "Super IA";
    public override string Description => "Ventaja de tipo + te remata con lo justo";

    public SuperAI(EffectivenessTable table) : base(table)
    {
    }

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
