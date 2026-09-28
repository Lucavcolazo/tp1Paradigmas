using ElementalGame.Models;
using ElementalGame.UI;

namespace ElementalGame.Players;

// [Inheritance] HumanPlayer is a Player: it reuses all of its logic and only defines how it chooses.
public class HumanPlayer : Player
{
    private readonly Screen screen;

    public HumanPlayer(string name, IEnumerable<ElementalUnit> units, Screen screen)
        : base(name, units)
    {
        this.screen = screen;
    }

    // [Polymorphism] Override: the human chooses by asking through the screen.
    protected override ElementalUnit ChooseUnit(IReadOnlyList<ElementalUnit> available, ElementalUnit? rival)
    {
        return screen.AskForUnit(available, rival);
    }
}
