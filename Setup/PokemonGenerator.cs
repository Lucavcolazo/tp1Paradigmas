using tp1Paradigmas.Models;

namespace tp1Paradigmas.Setup;

public class PokemonGenerator
{
    private static readonly ElementType[] Types = Enum.GetValues<ElementType>();
    private int lastId;

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
