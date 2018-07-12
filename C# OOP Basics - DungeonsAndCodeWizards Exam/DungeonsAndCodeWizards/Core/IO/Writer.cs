using System;

public class Writer : IWriter
{
    public void WriteLine(string text)
    {
        Console.WriteLine(text);
    }
}