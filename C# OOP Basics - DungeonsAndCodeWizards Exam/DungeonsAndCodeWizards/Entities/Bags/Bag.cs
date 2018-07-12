using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class Bag
{
    public int Capacity { get; }

    public int Load => this.Items.Sum(x => x.Weight);

    private List<Item> items;

    public IReadOnlyCollection<Item> Items => this.items.AsReadOnly();

    protected Bag(int capacity)
    {
        this.Capacity = capacity;
        this.items = new List<Item>();
    }

    public void AddItem(Item item)
    {
        if(this.Load + item.Weight > Capacity)
        {
            throw new InvalidOperationException("Bag is full!");
        }
        this.items.Add(item);
    }

    public Item GetItem(string name)
    {
        if(this.items.Count == 0)
        {
            throw new InvalidOperationException("Bag is empty!");
        }
        if(this.items.FirstOrDefault(x => x.GetType().Name == name) == null)
        {
            throw new ArgumentException($"No item with name {name} in bag!");
        }
        Item item = this.items.First(x => x.GetType().Name == name);
        items.Remove(item);
        return item;
    }
}
