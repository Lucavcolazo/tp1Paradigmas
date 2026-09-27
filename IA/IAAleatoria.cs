using JuegoElementos.Modelo;

namespace JuegoElementos.IA;

// Elige al azar, sin mirar al rival.
public class IAAleatoria : IEstrategiaIA
{
    public string Nombre => "Aleatoria";

    public UnidadElemental Elegir(IReadOnlyList<UnidadElemental> disponibles, UnidadElemental? rival)
    {
        return disponibles[Random.Shared.Next(disponibles.Count)];
    }
}
