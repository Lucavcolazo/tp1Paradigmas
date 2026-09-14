import java.util.EnumMap;
import java.util.Map;

public class TablaEfectividad {

    private final Map<TipoElemento, Map<TipoElemento, Double>> danios;

    public TablaEfectividad() {
        danios = new EnumMap<>(TipoElemento.class);
        for (TipoElemento tipo : TipoElemento.values()) {
            danios.put(tipo, new EnumMap<>(TipoElemento.class));
        }
        configurarDanioBase();
    }

    private void configurarDanioBase() {
        agregarRegla(TipoElemento.AGUA, TipoElemento.FUEGO, 50.0);
        agregarRegla(TipoElemento.FUEGO, TipoElemento.AGUA, 20.0);
        agregarRegla(TipoElemento.FUEGO, TipoElemento.TIERRA, 40.0);
        agregarRegla(TipoElemento.TIERRA, TipoElemento.FUEGO, 20.0);
        agregarRegla(TipoElemento.TIERRA, TipoElemento.AGUA, 50.0);
        agregarRegla(TipoElemento.AGUA, TipoElemento.TIERRA, 20.0);

        // Enfrentamientos entre el mismo tipo: dano parejo para que la ronda avance.
        agregarRegla(TipoElemento.AGUA, TipoElemento.AGUA, 15.0);
        agregarRegla(TipoElemento.TIERRA, TipoElemento.TIERRA, 15.0);
        agregarRegla(TipoElemento.FUEGO, TipoElemento.FUEGO, 15.0);
    }

    public void agregarRegla(TipoElemento atacante, TipoElemento defensor, double danio) {
        danios.get(atacante).put(defensor, danio);
    }

    public double obtenerDanio(TipoElemento atacante, TipoElemento defensor) {
        return danios.get(atacante).getOrDefault(defensor, 0.0);
    }
}
