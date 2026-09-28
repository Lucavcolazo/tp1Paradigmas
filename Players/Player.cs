using tp1Paradigmas.Models;

namespace tp1Paradigmas.Players;

// [Abstracción] Modela lo que todo jugador tiene en común (unidades, unidad activa, reemplazar a la caída)
// y deja abstracto lo único que cambia: cómo elige cada uno su unidad.
public abstract class Player
{
    // [Encapsulamiento] La lista es privada y se expone solo como vistas de solo lectura (Units, AliveUnits),
    // así nadie desde afuera puede agregar o quitar unidades.
    private readonly List<ElementalUnit> units;

    public string Name { get; }

    // [Encapsulamiento] Setter privado: solo el propio jugador decide qué unidad está activa.
    public ElementalUnit? ActiveUnit { get; private set; }

    public IReadOnlyList<ElementalUnit> Units => units.AsReadOnly();
    public IReadOnlyList<ElementalUnit> AliveUnits => units.Where(u => !u.IsKnockedOut).ToList();
    public bool HasAliveUnits => units.Any(u => !u.IsKnockedOut);

    private bool NeedsUnit => ActiveUnit is null || ActiveUnit.IsKnockedOut;

    protected Player(string name, IEnumerable<ElementalUnit> units)
    {
        Name = name;
        this.units = units.ToList();
    }

    // Si la unidad activa quedó fuera de combate (o todavía no hay una), elige un reemplazo entre las vivas.
    // [Polimorfismo] Llama a ChooseUnit() sin saber si es un humano o una IA: en tiempo de ejecución
    // se ejecuta la versión de la subclase real (template method).
    public void PrepareUnit(ElementalUnit? rival)
    {
        if (NeedsUnit && HasAliveUnits)
        {
            ActiveUnit = ChooseUnit(AliveUnits, rival);
        }
    }

    // [Abstracción] Método abstracto: obliga a cada subclase a definir cómo elige.
    protected abstract ElementalUnit ChooseUnit(IReadOnlyList<ElementalUnit> available, ElementalUnit? rival);
}
