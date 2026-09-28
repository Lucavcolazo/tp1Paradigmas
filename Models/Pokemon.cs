namespace ElementalGame.Models;

// [Inheritance] Pokemon is an ElementalUnit: it inherits its type, energy, Attack() and TakeDamage().
public class Pokemon : ElementalUnit
{
    public Pokemon(int id, ElementType type) : base(id, type)
    {
    }
}
