using JuegoElementos.IA;
using JuegoElementos.Jugadores;
using JuegoElementos.Modelo;

namespace JuegoElementos;

public class Program
{
    private const int CantidadDeUnidades = 5;

    public static void Main()
    {
        var tabla = TablaEfectividad.PorDefecto();
        var fabrica = new FabricaDePokemones();
        var estrategia = new SelectorDeEstrategia(tabla).ElegirAlAzar();

        var humano = new JugadorHumano("Jugador", fabrica.Generar(CantidadDeUnidades));
        var ia = new JugadorIA(estrategia, fabrica.Generar(CantidadDeUnidades));

        new Partida(humano, ia, tabla).Jugar();
    }
}
