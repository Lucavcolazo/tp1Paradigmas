using ElementalGame.Models;
using ElementalGame.Players;
using ElementalGame.UI;

namespace ElementalGame.Game;

public class Match
{
    // [Polimorfismo] Los dos se declaran como Player: la partida no distingue entre humano
    // e IA, solo le pide a cada uno que prepare su unidad y ataque.
    // [Encapsulamiento] Todo el estado es private y readonly.
    private readonly Player human;
    private readonly Player ai;
    private readonly EffectivenessTable table;
    private readonly Screen screen;

    private bool BothHaveUnits => human.HasAliveUnits && ai.HasAliveUnits;
    private int TotalAliveUnits => human.AliveUnits.Count + ai.AliveUnits.Count;

    public Match(Player human, Player ai, EffectivenessTable table, Screen screen)
    {
        this.human = human;
        this.ai = ai;
        this.table = table;
        this.screen = screen;
    }

    public void Play()
    {
        screen.ShowRival(ai);
        screen.ShowTable(table);
        screen.ShowStatus(human, ai);

        int roundNumber = 0;
        while (BothHaveUnits)
        {
            roundNumber++;
            screen.ShowRound(roundNumber);
            PlayRound();
        }

        var winner = human.HasAliveUnits ? human : ai;
        screen.ShowResult(winner, winner == human, roundNumber);
    }

    // El humano ataca primero. Si la unidad de la IA cae, la reemplaza y el reemplazo contraataca.
    // Al final muestra un resumen; los equipos completos solo cuando alguien quedó fuera de combate.
    private void PlayRound()
    {
        PrepareUnit(human, ai);
        PrepareUnit(ai, human);
        int aliveAtStart = TotalAliveUnits;

        double dealt = Attack(human, ai);

        PrepareUnit(ai, human);
        double taken = ai.HasAliveUnits ? Attack(ai, human) : 0;

        screen.ShowSummary(human, ai, dealt, taken);
        if (TotalAliveUnits < aliveAtStart)
        {
            screen.ShowStatus(human, ai);
        }
        if (BothHaveUnits)
        {
            screen.WaitToContinue();
        }
    }

    private void PrepareUnit(Player player, Player rival)
    {
        var previous = player.ActiveUnit;
        player.PrepareUnit(rival.ActiveUnit);
        if (player.ActiveUnit != previous)
        {
            screen.ShowEntry(player);
        }
    }

    private double Attack(Player attacker, Player defender)
    {
        var attackingUnit = attacker.ActiveUnit!;
        var defendingUnit = defender.ActiveUnit!;

        double damage = attackingUnit.Attack(defendingUnit, table);
        screen.ShowAttack(attacker, attackingUnit, defendingUnit, damage);
        return damage;
    }
}
