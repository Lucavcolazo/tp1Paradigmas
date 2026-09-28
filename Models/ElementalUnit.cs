namespace tp1Paradigmas.Models;

public abstract class ElementalUnit
{
    public const double MaxEnergy = 100.0;

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
