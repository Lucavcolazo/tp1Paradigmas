using ElementalGame.Models;

namespace ElementalGame.Setup;

public class PokemonGenerator
{
    private static readonly ElementType[] Types = Enum.GetValues<ElementType>();
    // [Encapsulation] The id counter is internal state that only the generator manages.
    private int lastId;

    // [Polymorphism] Returns ElementalUnit (the base type), not Pokemon: the rest of the game
    // works with the abstraction and doesn't depend on the concrete class.
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
