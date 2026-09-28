using ElementalGame.Models;

namespace ElementalGame.AI;

// Estrategia + eficiencia: si alguna unidad puede rematar al rival de un golpe, usa la que
// lo logra con menos ventaja y menos energía, guardando las mejores para después.
// Si ninguna lo remata, se comporta como la IA estratégica.
// [Herencia] SuperAI es una StrategicAI "con algo más": reutiliza la tabla y
// DamageDealt() sin duplicar código.
public class SuperAI : StrategicAI
{
    // [Polimorfismo] Sobrescribe el nombre y la descripción de la clase base.
    public override string Name => "Super IA";
    public override string Description => "Ventaja de tipo + te remata con lo justo";

    public SuperAI(EffectivenessTable table) : base(table)
    {
    }

    // [Polimorfismo] Sobrescribe Choose(), y cuando no puede rematar al rival vuelve
    // al comportamiento del padre con base.Choose().
    public override ElementalUnit Choose(IReadOnlyList<ElementalUnit> available, ElementalUnit? rival)
    {
        var finisher = rival is null
            ? null
            : available
                .Where(u => DamageDealt(u, rival) >= rival.Energy)
                .OrderBy(u => DamageDealt(u, rival))
                .ThenBy(u => u.Energy)
                .FirstOrDefault();

        return finisher ?? base.Choose(available, rival);
    }
}
