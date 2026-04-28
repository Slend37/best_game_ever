namespace BestGameEver.Core.Components;

public interface IGameInputHandler
{
    ConsoleKey GetKey();
    bool IsExitKey(ConsoleKey key);
}