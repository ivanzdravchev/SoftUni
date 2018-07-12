using System;
using System.Collections.Generic;
using System.Text;

public class Cleric : Character, IHealable
{
    public Cleric(string name, Faction faction)
        : base(name, 50, 25, 40, new Backpack(), faction)
    {
    }

    public override double RestHealMultiplier => 0.5;

    public void Heal(Character character)
    {
        EnsureAlive();
        if (!character.IsAlive)
        {
            throw new InvalidOperationException("Must be alive to perform this action!");
        }
        if(this.Faction != character.Faction)
        {
            throw new InvalidOperationException($"Cannot heal enemy character!");
        }
        character.Health += this.AbilityPoints;
    }
}