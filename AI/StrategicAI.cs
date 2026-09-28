using tp1Paradigmas.Models;

namespace tp1Paradigmas.AI;

public class StrategicAI : IStrategy
{
    protected EffectivenessTable Table { get; }

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

    protected double DamageDealt(ElementalUnit own, ElementalUnit rival) =>
        Table.GetDamage(own.Type, rival.Type);

    protected double DamageTaken(ElementalUnit own, ElementalUnit rival) =>
        Table.GetDamage(rival.Type, own.Type);

    private double Advantage(ElementalUnit own, ElementalUnit rival) =>
        DamageDealt(own, rival) - DamageTaken(own, rival);
}
