namespace JuegoElementos.Modelo;

public abstract class UnidadElemental
{
    public const double EnergiaMaxima = 100.0;

    public string Nombre { get; }
    public TipoElemento Tipo { get; }
    public double Energia { get; private set; }
    public bool EstaFueraDeCombate => Energia <= 0;

    protected UnidadElemental(string nombre, TipoElemento tipo)
    {
        Nombre = nombre;
        Tipo = tipo;
        Energia = EnergiaMaxima;
    }

    // Ataca al defensor segun la tabla y devuelve cuanta energia le saco realmente.
    public double Atacar(UnidadElemental defensor, TablaEfectividad tabla)
    {
        double energiaAntes = defensor.Energia;
        defensor.RecibirDanio(tabla.ObtenerDanio(Tipo, defensor.Tipo));
        return energiaAntes - defensor.Energia;
    }

    public void RecibirDanio(double porcentaje)
    {
        Energia = Math.Max(0, Energia - porcentaje);
    }

    public override string ToString() => $"{Nombre} ({Tipo}) - Energia: {Energia:0}%";
}
