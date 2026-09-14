import java.util.Scanner;

public class Partida {

    private static final int CANTIDAD_POKEMONES = 5;

    private final Jugador jugadorHumano;
    private final Jugador jugadorIA;
    private final TablaEfectividad tablaEfectividad;

    private UnidadElemental pokemonHumanoActual;
    private UnidadElemental pokemonIAActual;

    public Partida(Scanner scanner) {
        FabricaDePokemones fabrica = new FabricaDePokemones();
        this.jugadorHumano = new JugadorHumano("Jugador", fabrica.generar(CANTIDAD_POKEMONES), scanner);
        this.jugadorIA = new JugadorIA("IA", fabrica.generar(CANTIDAD_POKEMONES), new IAAleatoria());
        this.tablaEfectividad = new TablaEfectividad();
    }

    public void iniciar() {
        System.out.println("=== Comienza la partida ===");
        mostrarEstado();

        int numeroRonda = 1;
        while (jugadorHumano.tieneUnidadesVivas() && jugadorIA.tieneUnidadesVivas()) {
            System.out.println("\n--- Ronda " + numeroRonda + " ---");
            jugarRonda();
            numeroRonda++;
        }

        mostrarGanador();
    }

    private void jugarRonda() {
        asegurarPokemonesActivos();

        System.out.println(jugadorHumano.getNombre() + " ataca con " + pokemonHumanoActual.getNombre());
        double danioAIA = tablaEfectividad.obtenerDanio(pokemonHumanoActual.getTipo(), pokemonIAActual.getTipo());
        pokemonIAActual.recibirDanio(danioAIA);
        System.out.println("-> " + pokemonIAActual.getNombre() + " pierde " + danioAIA + "% de energia");

        if (pokemonIAActual.estaFueraDeCombate()) {
            System.out.println(pokemonIAActual.getNombre() + " quedo fuera de combate");
        } else {
            System.out.println(jugadorIA.getNombre() + " contraataca con " + pokemonIAActual.getNombre());
            double danioAHumano = tablaEfectividad.obtenerDanio(pokemonIAActual.getTipo(), pokemonHumanoActual.getTipo());
            pokemonHumanoActual.recibirDanio(danioAHumano);
            System.out.println("-> " + pokemonHumanoActual.getNombre() + " pierde " + danioAHumano + "% de energia");

            if (pokemonHumanoActual.estaFueraDeCombate()) {
                System.out.println(pokemonHumanoActual.getNombre() + " quedo fuera de combate");
            }
        }

        mostrarEstado();
    }

    private void asegurarPokemonesActivos() {
        if (pokemonHumanoActual == null || pokemonHumanoActual.estaFueraDeCombate()) {
            pokemonHumanoActual = jugadorHumano.elegirUnidad(pokemonIAActual);
        }
        if (pokemonIAActual == null || pokemonIAActual.estaFueraDeCombate()) {
            pokemonIAActual = jugadorIA.elegirUnidad(pokemonHumanoActual);
        }
    }

    private void mostrarEstado() {
        System.out.println();
        jugadorHumano.mostrarUnidades();
        jugadorIA.mostrarUnidades();
    }

    private void mostrarGanador() {
        System.out.println("\n=== Fin de la partida ===");
        if (jugadorHumano.tieneUnidadesVivas()) {
            System.out.println("Gano " + jugadorHumano.getNombre() + "!");
        } else {
            System.out.println("Gano " + jugadorIA.getNombre() + "!");
        }
    }
}
