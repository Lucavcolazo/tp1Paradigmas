using JuegoElementos.Modelo;

namespace JuegoElementos.IA;

public interface IEstrategiaIA
{
    string Nombre { get; }
    UnidadElemental Elegir(IReadOnlyList<UnidadElemental> disponibles, UnidadElemental? rival);
}
