import java.util.ArrayList;
import java.util.List;
import java.util.Random;

public class FabricaDePokemones {

    private final Random random = new Random();
    private int contador = 0;

    public List<UnidadElemental> generar(int cantidad) {
        List<UnidadElemental> pokemones = new ArrayList<>();
        for (int i = 0; i < cantidad; i++) {
            TipoElemento tipo = tipoAleatorio();
            pokemones.add(new Pokemon(nombrePara(tipo), tipo));
        }
        return pokemones;
    }

    private TipoElemento tipoAleatorio() {
        TipoElemento[] tipos = TipoElemento.values();
        return tipos[random.nextInt(tipos.length)];
    }

    private String nombrePara(TipoElemento tipo) {
        contador++;
        return "Pokemon " + tipo.name() + " #" + contador;
    }
}
