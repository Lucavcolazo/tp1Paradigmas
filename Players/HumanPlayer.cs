using tp1Paradigmas.Models;
using tp1Paradigmas.UI;

namespace tp1Paradigmas.Players;

// [Herencia] HumanPlayer es un Player: reutiliza toda su lógica y solo define cómo elige.
public class HumanPlayer : Player
{
    private readonly Screen screen;

    public HumanPlayer(string name, IEnumerable<ElementalUnit> units, Screen screen)
        : base(name, units)
    {
        this.screen = screen;
    }

    // [Polimorfismo] Override: el humano elige preguntando por pantalla.
    protected override ElementalUnit ChooseUnit(IReadOnlyList<ElementalUnit> available, ElementalUnit? rival)
    {
        return screen.AskForUnit(available, rival);
    }
}
