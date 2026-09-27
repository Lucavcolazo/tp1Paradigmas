using JuegoElementos.Modelo;

namespace JuegoElementos.Jugadores;

public abstract class Jugador
{
    private readonly List<UnidadElemental> unidades;

    public string Nombre { get; }
    public UnidadElemental? UnidadActiva { get; private set; }

    public IReadOnlyList<UnidadElemental> UnidadesVivas => unidades.Where(u => !u.EstaFueraDeCombate).ToList();
    public bool TieneUnidadesVivas => unidades.Any(u => !u.EstaFueraDeCombate);

    private bool NecesitaUnidad => UnidadActiva is null || UnidadActiva.EstaFueraDeCombate;

    protected Jugador(string nombre, IEnumerable<UnidadElemental> unidades)
    {
        Nombre = nombre;
        this.unidades = unidades.ToList();
    }

    // Si la unidad activa murio (o todavia no hay una), elige un reemplazo entre las vivas.
    public void PrepararUnidad(UnidadElemental? rival)
    {
        if (NecesitaUnidad && TieneUnidadesVivas)
        {
            UnidadActiva = ElegirUnidad(UnidadesVivas, rival);
        }
    }

    protected abstract UnidadElemental ElegirUnidad(IReadOnlyList<UnidadElemental> disponibles, UnidadElemental? rival);

    public void MostrarUnidades()
    {
        Console.WriteLine($"{Nombre}:");
        foreach (var unidad in unidades)
        {
            string marca = unidad == UnidadActiva ? ">" : " ";
            string estado = unidad.EstaFueraDeCombate ? " [fuera de combate]" : "";
            Console.WriteLine($" {marca} {unidad}{estado}");
        }
    }
}
