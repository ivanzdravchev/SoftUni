using System;
using System.Collections.Generic;
using System.Text;

public class PoisonPotion : Item
{
    private const int Damage = 20;

    public PoisonPotion()
        : base(5)
    {
    }

    public override void AffectCharacter(Character character)
    {
        base.AffectCharacter(character);
        character.Health = Math.Max(0, character.Health - Damage);
        if(character.Health == 0)
        {
            character.IsAlive = false;
        }
    }
}