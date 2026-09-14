import java.util.List;
import java.util.Random;

public class IAAleatoria implements EstrategiaIA {

    private final Random random = new Random();

    @Override
    public UnidadElemental elegir(List<UnidadElemental> unidadesPropias, UnidadElemental unidadRival) {
        int indice = random.nextInt(unidadesPropias.size());
        return unidadesPropias.get(indice);
    }
}
