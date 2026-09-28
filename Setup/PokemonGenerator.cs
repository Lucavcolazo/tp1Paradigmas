using tp1Paradigmas.Models;

namespace tp1Paradigmas.Setup;

public class PokemonGenerator
{
    private static readonly ElementType[] Types = Enum.GetValues<ElementType>();
    // [Encapsulamiento] El contador de ids es estado interno que solo maneja el generador.
    private int lastId;

    // [Polimorfismo] Devuelve ElementalUnit (el tipo base), no Pokemon: el resto del juego
    // trabaja con la abstracción y no depende de la clase concreta.
    public List<ElementalUnit> Generate(int count)
    {
        var pokemons = new List<ElementalUnit>();
        for (int i = 0; i < count; i++)
        {
            ElementType type = Types[Random.Shared.Next(Types.Length)];
            pokemons.Add(new Pokemon(++lastId, type));
        }
        return pokemons;
    }
}
