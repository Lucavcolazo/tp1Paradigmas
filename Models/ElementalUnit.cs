namespace ElementalGame.Models;

// [Abstraction] Represents the general idea of a combat unit. It is abstract, so it cannot be
// instantiated directly: only concrete units (like Pokemon) can exist.
public abstract class ElementalUnit
{
    public const double MaxEnergy = 100.0;

    // [Encapsulation] Read-only from outside. Energy has a private setter: nobody can assign it
    // directly, it only changes through TakeDamage(), which guarantees it never drops below 0.

    public int Id { get; }
    public ElementType Type { get; }
    public double Energy { get; private set; }
    public bool IsKnockedOut => Energy <= 0;

    protected ElementalUnit(int id, ElementType type)
    {
        Id = id;
        Type = type;
        Energy = MaxEnergy;
    }

    // Attacks the defender according to the table and returns how much energy it actually removed.
    // [Abstraction] Whoever calls Attack() doesn't need to know how the damage is calculated.
    public double Attack(ElementalUnit defender, EffectivenessTable table)
    {
        double energyBefore = defender.Energy;
        defender.TakeDamage(table.GetDamage(Type, defender.Type));
        return energyBefore - defender.Energy;
    }

    public void TakeDamage(double percentage)
    {
        Energy = Math.Max(0, Energy - percentage);
    }

    public override string ToString() => $"{Type} #{Id} - Energy: {Energy:0}%";
}
