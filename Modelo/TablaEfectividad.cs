namespace JuegoElementos.Modelo;

// Matriz configurable de dano: cambiar las reglas no afecta la logica del juego.
public class TablaEfectividad
{
    private readonly Dictionary<(TipoElemento Atacante, TipoElemento Defensor), double> danios = new();

    public static TablaEfectividad PorDefecto()
    {
        var tabla = new TablaEfectividad();
        tabla.AgregarRegla(TipoElemento.Agua, TipoElemento.Fuego, 50.0);
        tabla.AgregarRegla(TipoElemento.Fuego, TipoElemento.Agua, 20.0);
        tabla.AgregarRegla(TipoElemento.Fuego, TipoElemento.Tierra, 40.0);
        tabla.AgregarRegla(TipoElemento.Tierra, TipoElemento.Fuego, 20.0);
        tabla.AgregarRegla(TipoElemento.Tierra, TipoElemento.Agua, 50.0);
        tabla.AgregarRegla(TipoElemento.Agua, TipoElemento.Tierra, 20.0);

        // Enfrentamientos entre el mismo tipo: dano parejo para que la ronda avance.
        tabla.AgregarRegla(TipoElemento.Agua, TipoElemento.Agua, 15.0);
        tabla.AgregarRegla(TipoElemento.Tierra, TipoElemento.Tierra, 15.0);
        tabla.AgregarRegla(TipoElemento.Fuego, TipoElemento.Fuego, 15.0);
        return tabla;
    }

    public void AgregarRegla(TipoElemento atacante, TipoElemento defensor, double danio)
    {
        danios[(atacante, defensor)] = danio;
    }

    public double ObtenerDanio(TipoElemento atacante, TipoElemento defensor)
    {
        return danios.GetValueOrDefault((atacante, defensor), 0.0);
    }

    public void Mostrar()
    {
        Console.WriteLine("Tabla de dano (atacante -> defensor):");
        foreach (var ((atacante, defensor), danio) in danios)
        {
            Console.WriteLine($"  {atacante,-6} -> {defensor,-6} {danio:0}%");
        }
    }
}
