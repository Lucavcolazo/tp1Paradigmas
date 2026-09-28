namespace tp1Paradigmas.Models;

// Matriz de daño configurable: cambiar las reglas no afecta la lógica del juego.
// [Abstracción] El resto del juego solo pregunta "¿cuánto daño le hace X a Y?" con GetDamage(),
// sin saber cómo se guardan las reglas. Esto es lo que evita cadenas de if/switch entre tipos.
public class EffectivenessTable
{
    // [Encapsulamiento] El diccionario es privado: las reglas solo se agregan o consultan con los métodos públicos.
    private readonly Dictionary<(ElementType Attacker, ElementType Defender), double> damages = new();

    public static EffectivenessTable CreateDefault()
    {
        var table = new EffectivenessTable();
        table.AddRule(ElementType.Water, ElementType.Fire, 50.0);
        table.AddRule(ElementType.Fire, ElementType.Water, 20.0);
        table.AddRule(ElementType.Fire, ElementType.Earth, 40.0);
        table.AddRule(ElementType.Earth, ElementType.Fire, 20.0);
        table.AddRule(ElementType.Earth, ElementType.Water, 50.0);
        table.AddRule(ElementType.Water, ElementType.Earth, 20.0);

        // Enfrentamientos entre el mismo tipo: daño parejo para que la ronda avance.
        table.AddRule(ElementType.Water, ElementType.Water, 15.0);
        table.AddRule(ElementType.Earth, ElementType.Earth, 15.0);
        table.AddRule(ElementType.Fire, ElementType.Fire, 15.0);
        return table;
    }

    public void AddRule(ElementType attacker, ElementType defender, double damage)
    {
        damages[(attacker, defender)] = damage;
    }

    public double GetDamage(ElementType attacker, ElementType defender)
    {
        return damages.GetValueOrDefault((attacker, defender), 0.0);
    }
}
