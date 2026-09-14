import java.util.List;
import java.util.Scanner;

public class JugadorHumano extends Jugador {

    private final Scanner scanner;

    public JugadorHumano(String nombre, List<UnidadElemental> unidades, Scanner scanner) {
        super(nombre, unidades);
        this.scanner = scanner;
    }

    @Override
    public UnidadElemental elegirUnidad(UnidadElemental rival) {
        List<UnidadElemental> vivas = getUnidadesVivas();

        if (rival != null) {
            System.out.print("Pokemon rival actual: ");
            rival.mostrarEstado();
        }

        System.out.println(getNombre() + ", elegi tu pokemon:");
        for (int i = 0; i < vivas.size(); i++) {
            System.out.print("  " + (i + 1) + ". ");
            vivas.get(i).mostrarEstado();
        }

        int opcion = leerOpcion(vivas.size());
        return vivas.get(opcion - 1);
    }

    private int leerOpcion(int cantidadDisponible) {
        int opcion = -1;
        while (opcion < 1 || opcion > cantidadDisponible) {
            System.out.print("Opcion: ");
            String entrada = scanner.nextLine().trim();
            try {
                opcion = Integer.parseInt(entrada);
            } catch (NumberFormatException e) {
                System.out.println("Ingresa un numero valido.");
            }
        }
        return opcion;
    }
}
