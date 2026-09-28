using tp1Paradigmas.Models;

namespace tp1Paradigmas.Players;

public abstract class Player
{
    private readonly List<ElementalUnit> units;

    public string Name { get; }

    public ElementalUnit? ActiveUnit { get; private set; }

    public IReadOnlyList<ElementalUnit> Units => units.AsReadOnly();
    public IReadOnlyList<ElementalUnit> AliveUnits => units.Where(u => !u.IsKnockedOut).ToList();
    public bool HasAliveUnits => units.Any(u => !u.IsKnockedOut);

    private bool NeedsUnit => ActiveUnit is null || ActiveUnit.IsKnockedOut;

    protected Player(string name, IEnumerable<ElementalUnit> units)
    {
        Name = name;
        this.units = units.ToList();
    }

    public void PrepareUnit(ElementalUnit? rival)
    {
        if (NeedsUnit && HasAliveUnits)
        {
            ActiveUnit = ChooseUnit(AliveUnits, rival);
        }
    }

    protected abstract ElementalUnit ChooseUnit(IReadOnlyList<ElementalUnit> available, ElementalUnit? rival);
}
