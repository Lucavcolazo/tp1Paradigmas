using JuegoElementos.Jugadores;
using JuegoElementos.Modelo;

namespace JuegoElementos;

public class Partida
{
    private readonly Jugador humano;
    private readonly Jugador ia;
    private readonly TablaEfectividad tabla;

    private bool AmbosTienenUnidades => humano.TieneUnidadesVivas && ia.TieneUnidadesVivas;

    public Partida(Jugador humano, Jugador ia, TablaEfectividad tabla)
    {
        this.humano = humano;
        this.ia = ia;
        this.tabla = tabla;
    }

    public void Jugar()
    {
        Console.WriteLine("=== Comienza la partida ===");
        Console.WriteLine($"Tu rival es: {ia.Nombre}\n");
        tabla.Mostrar();
        MostrarEstado();

        int numeroRonda = 1;
        while (AmbosTienenUnidades)
        {
            Console.WriteLine($"\n--- Ronda {numeroRonda} ---");
            JugarRonda();
            numeroRonda++;
        }

        MostrarGanador();
    }

    // El humano ataca primero. Si la unidad de la IA cae, la reemplaza y el reemplazo contraataca.
    private void JugarRonda()
    {
        humano.PrepararUnidad(ia.UnidadActiva);
        ia.PrepararUnidad(humano.UnidadActiva);

        Console.WriteLine();
        Atacar(humano, ia);

        ia.PrepararUnidad(humano.UnidadActiva);
        if (ia.TieneUnidadesVivas)
        {
            Atacar(ia, humano);
        }

        MostrarEstado();
    }

    private void Atacar(Jugador atacante, Jugador defensor)
    {
        var unidadAtacante = atacante.UnidadActiva!;
        var unidadDefensora = defensor.UnidadActiva!;

        double danio = unidadAtacante.Atacar(unidadDefensora, tabla);
        Console.WriteLine($"{unidadAtacante.Nombre} ({atacante.Nombre}) ataca a {unidadDefensora.Nombre} y le quita {danio:0}%");

        if (unidadDefensora.EstaFueraDeCombate)
        {
            Console.WriteLine($"  -> {unidadDefensora.Nombre} quedo fuera de combate");
        }
    }

    private void MostrarEstado()
    {
        Console.WriteLine();
        humano.MostrarUnidades();
        ia.MostrarUnidades();
    }

    private void MostrarGanador()
    {
        var ganador = humano.TieneUnidadesVivas ? humano : ia;
        Console.WriteLine("\n=== Fin de la partida ===");
        Console.WriteLine($"Gano {ganador.Nombre}!");
    }
}
