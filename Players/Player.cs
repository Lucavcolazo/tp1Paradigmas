using ElementalGame.Models;

namespace ElementalGame.Players;

// [Abstraction] Models what every player has in common (units, active unit, replacing a fallen one)
// and leaves abstract the only thing that changes: how each one chooses its unit.
public abstract class Player
{
    // [Encapsulation] The list is private and exposed only as read-only views (Units, AliveUnits),
    // so nobody from outside can add or remove units.
    private readonly List<ElementalUnit> units;

    public string Name { get; }

    // [Encapsulation] Private setter: only the player itself decides which unit is active.
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

    // If the active unit was knocked out (or there is none yet), picks a replacement among the alive ones.
    // [Polymorphism] Calls ChooseUnit() without knowing whether it is a human or an AI: at runtime
    // the version of the actual subclass runs (template method).
    public void PrepareUnit(ElementalUnit? rival)
    {
        if (NeedsUnit && HasAliveUnits)
        {
            ActiveUnit = ChooseUnit(AliveUnits, rival);
        }
    }

    // [Abstraction] Abstract method: each subclass is forced to define how it chooses.
    protected abstract ElementalUnit ChooseUnit(IReadOnlyList<ElementalUnit> available, ElementalUnit? rival);
}
