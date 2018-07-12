using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

public class CharacterFactory
{
    public Character CreateCharacter(string faction, string type, string name)
    {
        if (!Enum.TryParse<Faction>(faction, out var parsedFaction))
        {
            throw new ArgumentException($"Invalid faction \"{faction}\"!");
        }
    
        Character character;
        switch (type)
        {
            case "Warrior":
                character = new Warrior(name, parsedFaction);
                break;
            case "Cleric":
                character = new Cleric(name, parsedFaction);
                break;
            default:
                throw new ArgumentException($"Invalid character type \"{type}\"!");
        }
    
        return character;
    }

    //public Character CreateCharacter(string faction, string type, string name)
    //{
    //    if (!Enum.TryParse(faction, out Faction parsedFaction))
    //    {
    //        throw new ArgumentException($"Invalid faction \"{faction}\"!");
    //    }
    //
    //    Type characterType = Assembly.GetExecutingAssembly().GetTypes()
    //        .FirstOrDefault(x => x.Name == type);
    //
    //    if (characterType == null)
    //    {
    //        throw new ArgumentException($"Invalid character type \"{type}\"!");
    //    }
    //
    //    Character character = (Character)Activator.CreateInstance(characterType, name, parsedFaction);
    //    return character;
    //}

}