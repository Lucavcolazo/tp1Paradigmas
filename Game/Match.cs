using ElementalGame.Models;
using ElementalGame.Players;
using ElementalGame.UI;

namespace ElementalGame.Game;

public class Match
{
    // [Polymorphism] Both are declared as Player: the match doesn't distinguish between human
    // and AI, it just asks each one to prepare its unit and attack.
    // [Encapsulation] All state is private and readonly.
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

    // The human attacks first. If the AI's unit falls, it gets replaced and the replacement counterattacks.
    // Shows a summary at the end; full teams only when someone was knocked out.
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
