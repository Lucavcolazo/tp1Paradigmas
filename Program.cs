using ElementalGame.Game;
using ElementalGame.Models;
using ElementalGame.Players;
using ElementalGame.Setup;
using ElementalGame.UI;

namespace ElementalGame;

public class Program
{
    private const int UnitsPerPlayer = 5;

    public static void Main()
    {
        var screen = new Screen();
        var table = EffectivenessTable.CreateDefault();
        var generator = new PokemonGenerator();

        screen.ShowWelcome();
        var strategy = screen.AskForStrategy(new StrategySelector(table));

        var human = new HumanPlayer("Jugador", generator.Generate(UnitsPerPlayer), screen);
        var ai = new AIPlayer(strategy, generator.Generate(UnitsPerPlayer));

        new Match(human, ai, table, screen).Play();
    }
}
