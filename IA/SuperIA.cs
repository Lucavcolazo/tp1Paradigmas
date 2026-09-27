using JuegoElementos.Modelo;

namespace JuegoElementos.IA;

// Estrategia + eficiencia: si alguna unidad puede rematar al rival de un golpe, usa la que
// lo logra con menos ventaja y menos energia, guardando las mejores para despues.
// Si ninguna lo remata, se comporta como la IA estrategica.
public class SuperIA : IAEstrategica
{
    public override string Nombre => "Super IA";

    public SuperIA(TablaEfectividad tabla) : base(tabla)
    {
    }

    public override UnidadElemental Elegir(IReadOnlyList<UnidadElemental> disponibles, UnidadElemental? rival)
    {
        var rematadora = rival is null
            ? null
            : disponibles
                .Where(u => DanioInfligido(u, rival) >= rival.Energia)
                .OrderBy(u => DanioInfligido(u, rival))
                .ThenBy(u => u.Energia)
                .FirstOrDefault();

        return rematadora ?? base.Elegir(disponibles, rival);
    }
}
