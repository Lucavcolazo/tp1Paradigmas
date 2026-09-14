import java.util.ArrayList;
import java.util.List;

public abstract class Jugador {

    private final String nombre;
    private final List<UnidadElemental> unidades;

    public Jugador(String nombre, List<UnidadElemental> unidades) {
        this.nombre = nombre;
        this.unidades = unidades;
    }

    public String getNombre() {
        return nombre;
    }

    public List<UnidadElemental> getUnidadesVivas() {
        List<UnidadElemental> vivas = new ArrayList<>();
        for (UnidadElemental unidad : unidades) {
            if (!unidad.estaFueraDeCombate()) {
                vivas.add(unidad);
            }
        }
        return vivas;
    }

    public boolean tieneUnidadesVivas() {
        return !getUnidadesVivas().isEmpty();
    }

    public void mostrarUnidades() {
        System.out.println(nombre + ":");
        for (UnidadElemental unidad : unidades) {
            System.out.print("  ");
            unidad.mostrarEstado();
        }
    }

    public abstract UnidadElemental elegirUnidad(UnidadElemental rival);
}
