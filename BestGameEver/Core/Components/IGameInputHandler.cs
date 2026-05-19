namespace BestGameEver.Core.Components;

public interface IGameInputHandler
{
    bool HasKey();
    ConsoleKey GetKey();
    bool IsExitKey(ConsoleKey key);
}