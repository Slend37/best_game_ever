namespace BestGameEver.Core.Components;

public class ConsoleInputHandler : IGameInputHandler
{
    public ConsoleKey GetKey()
    {
        return Console.ReadKey(true).Key;
    }
    
    public bool IsExitKey(ConsoleKey key)
    {
        return key == ConsoleKey.Escape;
    }
}