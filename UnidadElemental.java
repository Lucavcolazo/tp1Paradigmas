public abstract class UnidadElemental {

    private final String nombre;
    private final TipoElemento tipo;
    private double energia;

    public UnidadElemental(String nombre, TipoElemento tipo) {
        this.nombre = nombre;
        this.tipo = tipo;
        this.energia = 100.0;
    }

    public String getNombre() {
        return nombre;
    }

    public TipoElemento getTipo() {
        return tipo;
    }

    public double getEnergia() {
        return energia;
    }

    public void recibirDanio(double porcentaje) {
        energia -= porcentaje;
        if (energia < 0) {
            energia = 0;
        }
    }

    public boolean estaFueraDeCombate() {
        return energia <= 0;
    }

    public void mostrarEstado() {
        System.out.printf("%s (%s) - Energia: %.0f%%%n", nombre, tipo, energia);
    }
}
