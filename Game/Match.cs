using tp1Paradigmas.Models;
using tp1Paradigmas.Players;
using tp1Paradigmas.UI;

namespace tp1Paradigmas.Game;

public class Match
{
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
