import java.util.List;

public class JugadorIA extends Jugador {

    private final EstrategiaIA estrategia;

    public JugadorIA(String nombre, List<UnidadElemental> unidades, EstrategiaIA estrategia) {
        super(nombre, unidades);
        this.estrategia = estrategia;
    }

    @Override
    public UnidadElemental elegirUnidad(UnidadElemental rival) {
        return estrategia.elegir(getUnidadesVivas(), rival);
    }
}
