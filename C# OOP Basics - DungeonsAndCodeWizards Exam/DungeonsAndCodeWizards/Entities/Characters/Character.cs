using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

public abstract class Character
{
    private string name;
    private double health;
    private double armor;

    public double BaseHealth { get; private set; }

    public double BaseArmor { get; private set; }

    public double AbilityPoints { get; }

    public Bag Bag { get; }

    public Faction Faction { get; }

    public bool IsAlive { get; set; } = true;

    public virtual double RestHealMultiplier { get; } = 0.2;

    protected Character(string name, double health, double armor, double abilityPoints, Bag bag, Faction faction)
    {
        this.Name = name;

        this.BaseHealth = health;
        this.Health = health;

        this.BaseArmor = armor;
        this.Armor = armor;

        this.AbilityPoints = abilityPoints;
        this.Bag = bag;

        this.Faction = faction;
    }

    public double Armor
    {
        get { return this.armor; }
        set
        {
            this.armor = Math.Min(value, this.BaseArmor);
        }
    }

    public double Health
    {
        get { return this.health; }
        set
        {
            this.health = Math.Min(value, this.BaseHealth);
        }
    }

    public string Name
    {
        get { return this.name; }
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Name cannot be null or whitespace!");
            }
            this.name = value;
        }
    }

    public void TakeDamage(double hitPoints)
    {
        this.EnsureAlive();

        double hitpointsLeftAfterArmorDamage = Math.Max(0, hitPoints - this.Armor);
        this.Armor = Math.Max(0, this.Armor - hitPoints);
        this.Health = Math.Max(0, this.Health - hitpointsLeftAfterArmorDamage);

        if(Health == 0)
        {
            this.IsAlive = false;
        }
    }

    public void Rest()
    {
        EnsureAlive();
        this.Health += BaseHealth * RestHealMultiplier;
    }

    public void UseItem(Item item)
    {
        item.AffectCharacter(this);
    }

    public void UseItemOn(Item item, Character character)
    {
        character.UseItem(item);
    }

    public void GiveCharacterItem(Item item, Character character)
    {
        character.ReceiveItem(item);
    }

    public void ReceiveItem(Item item)
    {
        this.Bag.AddItem(item);
    }

    public override string ToString()
    {
        string format = "{0} - HP: {1}/{2}, AP: {3}/{4}, Status: {5}";
        string result = string.Format(format,
            this.Name,
            this.Health,
            this.BaseHealth,
            this.Armor,
            this.BaseArmor,
            this.IsAlive ? "Alive" : "Dead");
        return result;
    }

    protected void EnsureAlive()
    {
        if (!this.IsAlive)
        {
            throw new InvalidOperationException("Must be alive to perform this action!");
        }
    }
}