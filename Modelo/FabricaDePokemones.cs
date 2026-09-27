namespace JuegoElementos.Modelo;

public class FabricaDePokemones
{
    private static readonly TipoElemento[] Tipos = Enum.GetValues<TipoElemento>();
    private int contador;

    public List<UnidadElemental> Generar(int cantidad)
    {
        var pokemones = new List<UnidadElemental>();
        for (int i = 0; i < cantidad; i++)
        {
            TipoElemento tipo = Tipos[Random.Shared.Next(Tipos.Length)];
            pokemones.Add(new Pokemon(NombrePara(tipo), tipo));
        }
        return pokemones;
    }

    private string NombrePara(TipoElemento tipo)
    {
        contador++;
        return $"{tipo} #{contador}";
    }
}
