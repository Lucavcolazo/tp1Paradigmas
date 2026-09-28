using tp1Paradigmas.Models;

namespace tp1Paradigmas.AI;

// Elige la unidad con mejor balance entre daño infligido y daño recibido frente al rival.
// [Polimorfismo] Implementa IStrategy.
public class StrategicAI : IStrategy
{
    // [Encapsulamiento] protected: oculto desde afuera, pero accesible para las subclases (SuperAI).
    protected EffectivenessTable Table { get; }

    // [Polimorfismo] Miembros virtual: las subclases pueden redefinirlos.
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

    // [Encapsulamiento] Helpers protected: SuperAI los reutiliza, pero no son parte de la API pública.
    protected double DamageDealt(ElementalUnit own, ElementalUnit rival) =>
        Table.GetDamage(own.Type, rival.Type);

    protected double DamageTaken(ElementalUnit own, ElementalUnit rival) =>
        Table.GetDamage(rival.Type, own.Type);

    private double Advantage(ElementalUnit own, ElementalUnit rival) =>
        DamageDealt(own, rival) - DamageTaken(own, rival);
}
