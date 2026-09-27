using JuegoElementos.Modelo;

namespace JuegoElementos.IA;

// Elige la unidad con mejor balance entre dano infligido y dano recibido frente al rival.
public class IAEstrategica : IEstrategiaIA
{
    protected TablaEfectividad Tabla { get; }

    public virtual string Nombre => "Estrategica";

    public IAEstrategica(TablaEfectividad tabla)
    {
        Tabla = tabla;
    }

    public virtual UnidadElemental Elegir(IReadOnlyList<UnidadElemental> disponibles, UnidadElemental? rival)
    {
        return rival is null
            ? disponibles.MaxBy(u => u.Energia)!
            : disponibles
                .OrderByDescending(u => Ventaja(u, rival))
                .ThenByDescending(u => u.Energia)
                .First();
    }

    protected double DanioInfligido(UnidadElemental propia, UnidadElemental rival) =>
        Tabla.ObtenerDanio(propia.Tipo, rival.Tipo);

    protected double DanioRecibido(UnidadElemental propia, UnidadElemental rival) =>
        Tabla.ObtenerDanio(rival.Tipo, propia.Tipo);

    private double Ventaja(UnidadElemental propia, UnidadElemental rival) =>
        DanioInfligido(propia, rival) - DanioRecibido(propia, rival);
}
