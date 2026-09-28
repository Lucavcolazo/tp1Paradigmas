using tp1Paradigmas.Models;
using tp1Paradigmas.UI;

namespace tp1Paradigmas.Players;

public class HumanPlayer : Player
{
    private readonly Screen screen;

    public HumanPlayer(string name, IEnumerable<ElementalUnit> units, Screen screen)
        : base(name, units)
    {
        this.screen = screen;
    }

    protected override ElementalUnit ChooseUnit(IReadOnlyList<ElementalUnit> available, ElementalUnit? rival)
    {
        return screen.AskForUnit(available, rival);
    }
}
