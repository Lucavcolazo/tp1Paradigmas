namespace tp1Paradigmas.Models;

// [Herencia] Pokemon es una ElementalUnit: hereda su tipo, energía, Attack() y TakeDamage().
public class Pokemon : ElementalUnit
{
    public Pokemon(int id, ElementType type) : base(id, type)
    {
    }
}
