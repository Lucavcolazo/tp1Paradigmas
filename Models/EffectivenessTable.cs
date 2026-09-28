namespace tp1Paradigmas.Models;

public class EffectivenessTable
{
    private readonly Dictionary<(ElementType Attacker, ElementType Defender), double> damages = new();

    public static EffectivenessTable CreateDefault()
    {
        var table = new EffectivenessTable();
        table.AddRule(ElementType.Water, ElementType.Fire, 50.0);
        table.AddRule(ElementType.Fire, ElementType.Water, 20.0);
        table.AddRule(ElementType.Fire, ElementType.Earth, 40.0);
        table.AddRule(ElementType.Earth, ElementType.Fire, 20.0);
        table.AddRule(ElementType.Earth, ElementType.Water, 50.0);
        table.AddRule(ElementType.Water, ElementType.Earth, 20.0);

        table.AddRule(ElementType.Water, ElementType.Water, 15.0);
        table.AddRule(ElementType.Earth, ElementType.Earth, 15.0);
        table.AddRule(ElementType.Fire, ElementType.Fire, 15.0);
        return table;
    }

    public void AddRule(ElementType attacker, ElementType defender, double damage)
    {
        damages[(attacker, defender)] = damage;
    }

    public double GetDamage(ElementType attacker, ElementType defender)
    {
        return damages.GetValueOrDefault((attacker, defender), 0.0);
    }
}
