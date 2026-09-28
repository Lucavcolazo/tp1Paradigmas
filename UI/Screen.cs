using System.Text;
using ElementalGame.AI;
using ElementalGame.Models;
using ElementalGame.Players;
using ElementalGame.Setup;

namespace ElementalGame.UI;

// Everything shown in the terminal (and everything read from the human) goes through here.
// Player-facing text is in Spanish.
// [Encapsulation] Only exposes "show X" / "ask for Y" methods; colors, styles and drawing helpers
// are private. The model classes never write to the console.
public class Screen
{
    private const int BoxWidth = 44;
    private const int NameWidth = 12;
    private const int BarLength = 20;
    private const int ColumnWidth = 12;

    private static readonly Dictionary<ElementType, (string Icon, ConsoleColor Color, string Label)> Styles = new()
    {
        [ElementType.Water] = ("💧", ConsoleColor.Cyan, "Agua"),
        [ElementType.Earth] = ("🌱", ConsoleColor.Green, "Tierra"),
        [ElementType.Fire] = ("🔥", ConsoleColor.Red, "Fuego")
    };

    private static readonly (double Minimum, ConsoleColor Color)[] EnergyColors =
    {
        (60, ConsoleColor.Green),
        (30, ConsoleColor.Yellow),
        (0, ConsoleColor.Red)
    };

    private static readonly (double Minimum, ConsoleColor Color)[] DamageColors =
    {
        (40, ConsoleColor.Red),
        (20, ConsoleColor.Yellow),
        (0, ConsoleColor.DarkGray)
    };

    // Indexed by the sign of (damage dealt - damage taken) in the round.
    private static readonly Dictionary<int, (string Text, ConsoleColor Color)> Exchanges = new()
    {
        [1] = ("Ganaste el intercambio", ConsoleColor.Green),
        [0] = ("Intercambio parejo", ConsoleColor.Yellow),
        [-1] = ("Perdiste el intercambio", ConsoleColor.Red)
    };

    private readonly bool animate = !Console.IsOutputRedirected;
    private readonly bool waitForPlayer = !Console.IsInputRedirected;

    public Screen()
    {
        Console.OutputEncoding = Encoding.UTF8;
    }

    public void ShowWelcome()
    {
        Console.WriteLine();
        Write($"  ╔{new string('═', BoxWidth)}╗\n", ConsoleColor.DarkYellow);
        WriteBoxLine(ConsoleColor.DarkYellow);
        WriteBoxLine(ConsoleColor.DarkYellow,
            ("AGUA", Styles[ElementType.Water].Color),
            ("  ·  ", ConsoleColor.DarkGray),
            ("TIERRA", Styles[ElementType.Earth].Color),
            ("  ·  ", ConsoleColor.DarkGray),
            ("FUEGO", Styles[ElementType.Fire].Color));
        WriteBoxLine(ConsoleColor.DarkYellow, ("juego por turnos", ConsoleColor.Gray));
        WriteBoxLine(ConsoleColor.DarkYellow);
        Write($"  ╚{new string('═', BoxWidth)}╝\n", ConsoleColor.DarkYellow);
    }

    // Rival menu, with one extra option at the end to draw it at random.
    public IStrategy AskForStrategy(StrategySelector selector)
    {
        var strategies = selector.Strategies;
        int surpriseOption = strategies.Count + 1;

        WriteTitle("ELEGÍ TU RIVAL", "");
        for (int i = 0; i < strategies.Count; i++)
        {
            WriteRivalOption(i + 1, strategies[i].Name, strategies[i].Description);
        }
        WriteRivalOption(surpriseOption, "Sorpresa", "Te toca una de las anteriores al azar");

        int option = ReadOption(surpriseOption);
        return option == surpriseOption ? selector.PickRandom() : strategies[option - 1];
    }

    public void ShowRival(Player rival)
    {
        Console.WriteLine();
        Write("  Tu rival: ", ConsoleColor.Gray);
        Write($"{rival.Name}\n", ConsoleColor.Magenta);
        Pause(600);
    }

    public void ShowTable(EffectivenessTable table)
    {
        var types = Enum.GetValues<ElementType>();
        WriteTitle("TABLA DE DAÑO", "la fila ataca a la columna");

        Console.Write("  " + new string(' ', ColumnWidth));
        foreach (var defender in types)
        {
            Write(Label(defender).PadRight(ColumnWidth), Styles[defender].Color);
        }
        Console.WriteLine();

        foreach (var attacker in types)
        {
            Write("  " + Label(attacker).PadRight(ColumnWidth), Styles[attacker].Color);
            foreach (var defender in types)
            {
                double damage = table.GetDamage(attacker, defender);
                Write($"  {damage,3:0}%".PadRight(ColumnWidth), ColorFor(DamageColors, damage));
            }
            Console.WriteLine();
        }
        Pause(600);
    }

    public void ShowStatus(params Player[] players)
    {
        foreach (var player in players)
        {
            string standing = $"{player.AliveUnits.Count}/{player.Units.Count} en pie";
            WriteTitle(player.Name.ToUpper(), standing);
            foreach (var unit in player.Units)
            {
                bool active = unit == player.ActiveUnit && !unit.IsKnockedOut;
                Write(active ? "  ► " : "    ", ConsoleColor.Yellow);
                WriteUnit(unit);
                Console.WriteLine();
            }
        }
    }

    public void ShowSummary(Player human, Player rival, double dealt, double taken)
    {
        var (text, color) = Exchanges[Math.Sign(dealt - taken)];

        Console.WriteLine();
        Write($"  ┌─ Resumen {new string('─', BoxWidth - 9)}\n", ConsoleColor.DarkGray);
        WriteSummaryLine("Vos", human);
        WriteSummaryLine("Rival", rival);
        Write("  │ ", ConsoleColor.DarkGray);
        Write(text, color);
        Write($": sacaste {dealt:0}% y recibiste {taken:0}%\n", ConsoleColor.Gray);
        Write("  │ ", ConsoleColor.DarkGray);
        Write($"En pie: {human.AliveUnits.Count} vs {rival.AliveUnits.Count}\n", ConsoleColor.Gray);
        Write($"  └{new string('─', BoxWidth + 1)}\n", ConsoleColor.DarkGray);
    }

    public void WaitToContinue()
    {
        if (waitForPlayer)
        {
            Write("  Enter para continuar...", ConsoleColor.DarkGray);
            Console.ReadLine();
        }
    }

    public void ShowRound(int number)
    {
        string line = new('━', 15);
        Console.WriteLine();
        Console.WriteLine();
        Write($"  {line}  RONDA {number}  {line}\n", ConsoleColor.Yellow);
    }

    public void ShowEntry(Player player)
    {
        var unit = player.ActiveUnit!;
        Console.WriteLine();
        Write($"  {player.Name} ", ConsoleColor.Magenta);
        Write("envía a ", ConsoleColor.Gray);
        Write($"{Label(unit)}\n", ColorOf(unit));
        Pause(500);
    }

    public void ShowAttack(Player player, ElementalUnit attacker, ElementalUnit defender, double damage)
    {
        Console.WriteLine();
        Write($"  {player.Name} ataca\n", ConsoleColor.Magenta);
        Write($"  {Label(attacker)}", ColorOf(attacker));
        Write("  ──➜  ", ConsoleColor.DarkGray);
        Write(Label(defender), ColorOf(defender));
        Write($"   -{damage:0}%", ColorFor(DamageColors, damage));
        Write($"   (le queda {defender.Energy:0}%)\n", ConsoleColor.DarkGray);
        Pause(700);

        if (defender.IsKnockedOut)
        {
            Write($"  💀 {NameOf(defender)} quedó fuera de combate\n", ConsoleColor.Red);
            Pause(700);
        }
    }

    public ElementalUnit AskForUnit(IReadOnlyList<ElementalUnit> available, ElementalUnit? rival)
    {
        Console.WriteLine();
        if (rival is not null)
        {
            Write("  Rival en combate: ", ConsoleColor.Gray);
            WriteUnit(rival);
            Console.WriteLine();
        }

        Write("  Elegí tu elemento:\n", ConsoleColor.White);
        for (int i = 0; i < available.Count; i++)
        {
            Write($"    [{i + 1}] ", ConsoleColor.Yellow);
            WriteUnit(available[i]);
            Console.WriteLine();
        }

        return available[ReadOption(available.Count) - 1];
    }

    public void ShowResult(Player winner, bool humanWon, int rounds)
    {
        var color = humanWon ? ConsoleColor.Green : ConsoleColor.Red;
        string message = humanWon ? "¡GANASTE!" : "PERDISTE";

        Console.WriteLine();
        Console.WriteLine();
        Write($"  ╔{new string('═', BoxWidth)}╗\n", color);
        WriteBoxLine(color);
        WriteBoxLine(color, (message, color));
        WriteBoxLine(color, ($"Ganó {winner.Name} en {rounds} {(rounds == 1 ? "ronda" : "rondas")}", ConsoleColor.Gray));
        WriteBoxLine(color);
        Write($"  ╚{new string('═', BoxWidth)}╝\n\n", color);
    }

    private static int ReadOption(int optionCount)
    {
        while (true)
        {
            Write("  ❯ ", ConsoleColor.Yellow);
            string input = Console.ReadLine()
                ?? throw new InvalidOperationException("Standard input was closed.");

            if (int.TryParse(input.Trim(), out int option) && option >= 1 && option <= optionCount)
            {
                return option;
            }
            Write($"  Opción inválida, ingresá un número del 1 al {optionCount}.\n", ConsoleColor.Red);
        }
    }

    private static void WriteSummaryLine(string label, Player player)
    {
        Write("  │ ", ConsoleColor.DarkGray);
        Write(label.PadRight(7), ConsoleColor.Magenta);
        WriteUnit(player.ActiveUnit!);
        Console.WriteLine();
    }

    private static void WriteRivalOption(int number, string name, string description)
    {
        Write($"    [{number}] ", ConsoleColor.Yellow);
        Write(name.PadRight(16), ConsoleColor.Magenta);
        Write($"{description}\n", ConsoleColor.DarkGray);
    }

    private static void WriteUnit(ElementalUnit unit)
    {
        Write(Label(unit).PadRight(NameWidth + 3), ColorOf(unit));

        int filled = (int)Math.Round(unit.Energy / ElementalUnit.MaxEnergy * BarLength);
        var energyColor = ColorFor(EnergyColors, unit.Energy);
        Write(new string('█', filled), energyColor);
        Write(new string('░', BarLength - filled), ConsoleColor.DarkGray);

        Write(unit.IsKnockedOut ? "    KO" : $"  {unit.Energy,3:0}%",
            unit.IsKnockedOut ? ConsoleColor.DarkRed : energyColor);
    }

    private static void WriteTitle(string text, string detail)
    {
        int width = BoxWidth + 2;
        Console.WriteLine();
        Write($"  {text}", ConsoleColor.White);
        Write($"{detail.PadLeft(width - text.Length)}\n", ConsoleColor.DarkGray);
        Write($"  {new string('─', width)}\n", ConsoleColor.DarkGray);
    }

    private static void WriteBoxLine(ConsoleColor border, params (string Text, ConsoleColor Color)[] parts)
    {
        int length = parts.Sum(p => p.Text.Length);
        int left = (BoxWidth - length) / 2;

        Write("  ║" + new string(' ', left), border);
        foreach (var (text, color) in parts)
        {
            Write(text, color);
        }
        Write(new string(' ', BoxWidth - length - left) + "║\n", border);
    }

    private static string Label(ElementType type) => $"{Styles[type].Icon} {Styles[type].Label}";

    private static string Label(ElementalUnit unit) => $"{Styles[unit.Type].Icon} {NameOf(unit)}";

    private static string NameOf(ElementalUnit unit) => $"{Styles[unit.Type].Label} #{unit.Id}";

    private static ConsoleColor ColorOf(ElementalUnit unit) =>
        unit.IsKnockedOut ? ConsoleColor.DarkGray : Styles[unit.Type].Color;

    private static ConsoleColor ColorFor((double Minimum, ConsoleColor Color)[] scale, double value) =>
        scale.First(e => value >= e.Minimum).Color;

    private static void Write(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(text);
        Console.ResetColor();
    }

    private void Pause(int milliseconds)
    {
        if (animate)
        {
            Thread.Sleep(milliseconds);
        }
    }
}
