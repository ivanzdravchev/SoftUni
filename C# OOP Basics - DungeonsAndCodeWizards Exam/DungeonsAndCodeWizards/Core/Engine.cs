using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Engine
{
    private IReader reader;
    private IWriter writer;
    private DungeonMaster dungeonMaster;
    private bool IsRunning;

    public Engine(IReader reader, IWriter writer)
    {
        this.reader = reader;
        this.writer = writer;
        this.dungeonMaster = new DungeonMaster();
    }

    public void Run()
    {
        IsRunning = true;

        while (IsRunning)
        {
            string input = reader.ReadLine();
            try
            {
                ProcessCommand(input);
            }
            catch(ArgumentException ae)
            {
                writer.WriteLine($"Parameter Error: {ae.Message}");
            }
            catch(InvalidOperationException ioe)
            {
                writer.WriteLine($"Invalid Operation: {ioe.Message}");
            }
            if (dungeonMaster.IsGameOver() || this.IsRunning == false)
            {
                writer.WriteLine("Final stats:");
                writer.WriteLine(this.dungeonMaster.GetStats());
                IsRunning = false;
            }
        }
    }

    public void ProcessCommand(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            IsRunning = false;
            return;
        }
        string[] args = input.Split();
        string command = args[0];
        args = args.Skip(1).ToArray();
        switch (command)
        {
            case "JoinParty":
                writer.WriteLine(dungeonMaster.JoinParty(args));
                break;
            case "AddItemToPool":
                writer.WriteLine(dungeonMaster.AddItemToPool(args));
                break;
            case "PickUpItem":
                writer.WriteLine(dungeonMaster.PickUpItem(args));
                break;
            case "UseItem":
                writer.WriteLine(dungeonMaster.UseItem(args));
                break;
            case "UseItemOn":
                writer.WriteLine(dungeonMaster.UseItemOn(args));
                break;
            case "GiveCharacterItem":
                writer.WriteLine(dungeonMaster.GiveCharacterItem(args));
                break;
            case "GetStats":
                writer.WriteLine(dungeonMaster.GetStats());
                break;
            case "Attack":
                writer.WriteLine(dungeonMaster.Attack(args));
                break;
            case "Heal":
                writer.WriteLine(dungeonMaster.Heal(args));
                break;
            case "EndTurn":
                writer.WriteLine(dungeonMaster.EndTurn());
                break;
            case "IsGameOver":
                writer.WriteLine(dungeonMaster.IsGameOver().ToString());
                break;
        }
    }
}