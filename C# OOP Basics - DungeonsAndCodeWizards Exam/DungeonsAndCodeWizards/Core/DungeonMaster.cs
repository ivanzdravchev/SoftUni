using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class DungeonMaster
{
    private CharacterFactory characterFactory;
    private ItemFactory itemFactory;
    private List<Character> party;
    private Stack<Item> itemPool;
    private int lastSurvivorRounds = 0;

    public DungeonMaster()
    {
        this.characterFactory = new CharacterFactory();
        this.itemFactory = new ItemFactory();
        this.party = new List<Character>();
        this.itemPool = new Stack<Item>();
    }

    public string JoinParty(string[] args)
    {
        string faction = args[0];
        string charType = args[1];
        string name = args[2];
        this.party.Add(characterFactory.CreateCharacter(faction, charType, name));
        return $"{name} joined the party!";
    }

    public string AddItemToPool(string[] args)
    {
        string itemName = args[0];
        this.itemPool.Push(itemFactory.CreateItem(itemName));
        return $"{itemName} added to pool.";
    }

    public string PickUpItem(string[] args)
    {
        Character ch = FindCharacter(args[0]);

        if(this.itemPool.Count == 0)
        {
            throw new InvalidOperationException($"No items left in pool!");
        }

        Item item = itemPool.Pop();
        ch.ReceiveItem(item);
        return $"{ch.Name} picked up {item.GetType().Name}!";
    }

    public string UseItem(string[] args)
    {
        Character ch = FindCharacter(args[0]);
        string itemName = args[1];
        Item item = ch.Bag.GetItem(itemName);
        ch.UseItem(item);
        return $"{ch.Name} used {item.GetType().Name}.";
    }

    public string UseItemOn(string[] args)
    {
        string giverName = args[0];
        string receiverName = args[1];
        string itemName = args[2];

        Character chGiver = FindCharacter(giverName);
        Character chReceiver = FindCharacter(receiverName);

        Item item = chGiver.Bag.GetItem(itemName);
        chGiver.UseItemOn(item, chReceiver);
        return $"{giverName} used {itemName} on {receiverName}.";
    }

    public string GiveCharacterItem(string[] args)
    {
        string giverName = args[0];
        string receiverName = args[1];
        string itemName = args[2];

        Character chGiver = FindCharacter(giverName);
        Character chReceiver = FindCharacter(receiverName);

        Item item = chGiver.Bag.GetItem(itemName);
        chReceiver.ReceiveItem(item);

        return $"{giverName} gave {receiverName} {itemName}.";
    }

    public string GetStats()
    {
        StringBuilder sb = new StringBuilder();
        foreach(Character ch in this.party.OrderByDescending(x => x.IsAlive)
            .ThenByDescending(x => x.Health))
        {
            sb.AppendLine(ch.ToString());
        }
        return sb.ToString().Trim();
    }

    public string Attack(string[] args)
    {
        string attackerName = args[0];
        string receiverName = args[1];

        Character attacker = FindCharacter(attackerName);
        Character receiver = FindCharacter(receiverName);

        if(!(attacker is IAttackable attackingCharacter))
        {
            throw new ArgumentException($"{attackerName} cannot attack!");
        }
        attackingCharacter.Attack(receiver);
        
        StringBuilder sb = new StringBuilder();

        sb.AppendLine($"{attackerName} attacks {receiverName} for {attacker.AbilityPoints} hit points! {receiverName} has {receiver.Health}/{receiver.BaseHealth} HP and {receiver.Armor}/{receiver.BaseArmor} AP left!");
        if (!receiver.IsAlive)
        {
            sb.AppendLine($"{receiver.Name} is dead!");
        }

        return sb.ToString().Trim();
    }

    public string Heal(string[] args)
    {
        string healerName = args[0];
        string healingReceiverName = args[1];

        Character healer = FindCharacter(healerName);
        Character target = FindCharacter(healingReceiverName);

        if(!(healer is IHealable healingCharacter))
        {
            throw new ArgumentException($"{healerName} cannot heal!");
        }
        healingCharacter.Heal(target);
        string result = $"{healer.Name} heals {target.Name} for {healer.AbilityPoints}! {target.Name} has {target.Health} health now!";

        return result;
    }

    public string EndTurn()
    {
        StringBuilder sb = new StringBuilder();
        List<Character> aliveCharacters = party.Where(c => c.IsAlive).ToList();

        foreach(Character ch in aliveCharacters)
        {
            sb.Append($"{ch.Name} rests ({ch.Health} => ");
            ch.Rest();
            sb.AppendLine($"{ch.Health})");
        }

        if(aliveCharacters.Count <= 1)
        {
            this.lastSurvivorRounds++;
        }

        return sb.ToString().Trim();
    }

    public bool IsGameOver()
    {
        bool oneOrZeroSurvivorsLeft = this.party.Count(c => c.IsAlive) <= 1;
        bool lastSurvivorSurvivedLongEnough = this.lastSurvivorRounds > 1;

        return lastSurvivorSurvivedLongEnough && oneOrZeroSurvivorsLeft;
    }

    private Character FindCharacter(string name)
    {
        Character ch = this.party.FirstOrDefault(x => x.Name == name);
        if (ch == null)
        {
            throw new ArgumentException($"Character {name} not found!");
        }
        return ch;
    }
}