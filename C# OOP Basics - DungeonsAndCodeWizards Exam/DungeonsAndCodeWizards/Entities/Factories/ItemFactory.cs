using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

public class ItemFactory
{
    public Item CreateItem(string name)
    {
        Item item;
        switch (name)
        {
            case "HealthPotion":
                item = new HealthPotion();
                break;
            case "PoisonPotion":
                item = new PoisonPotion();
                break;
            case "ArmorRepairKit":
                item = new ArmorRepairKit();
                break;
            default:
                throw new ArgumentException($"Invalid item \"{name}\"!");
        }
    
        return item;
    }

    //public Item CreateItem(string name)
    //{
    //    Type itemType = Assembly.GetExecutingAssembly().GetTypes()
    //        .FirstOrDefault(x => x.Name == name);
    //
    //    if(itemType == null)
    //    {
    //        throw new ArgumentException($"Invalid item \"{name}\"!");
    //    }
    //
    //    Item item = (Item)Activator.CreateInstance(itemType);
    //    return item;
    //}
}