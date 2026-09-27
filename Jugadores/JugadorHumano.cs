using JuegoElementos.Modelo;

namespace JuegoElementos.Jugadores;

public class JugadorHumano : Jugador
{
    public JugadorHumano(string nombre, IEnumerable<UnidadElemental> unidades) : base(nombre, unidades)
    {
    }

    protected override UnidadElemental ElegirUnidad(IReadOnlyList<UnidadElemental> disponibles, UnidadElemental? rival)
    {
        Console.WriteLine();
        if (rival is not null)
        {
            Console.WriteLine($"Pokemon rival actual: {rival}");
        }

        Console.WriteLine($"{Nombre}, elegi tu pokemon:");
        for (int i = 0; i < disponibles.Count; i++)
        {
            Console.WriteLine($"  {i + 1}. {disponibles[i]}");
        }

        return disponibles[LeerOpcion(disponibles.Count) - 1];
    }

    private static int LeerOpcion(int cantidadDisponible)
    {
        while (true)
        {
            Console.Write("Opcion: ");
            string entrada = Console.ReadLine()
                ?? throw new InvalidOperationException("Se cerro la entrada estandar.");

            if (int.TryParse(entrada.Trim(), out int opcion) && opcion >= 1 && opcion <= cantidadDisponible)
            {
                return opcion;
            }
            Console.WriteLine($"Ingresa un numero entre 1 y {cantidadDisponible}.");
        }
    }
}
