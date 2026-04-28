namespace BestGameEver.Core.Components;

public interface IGameStateManager
{
    bool IsGameRunning { get; }
    void StopGame();
    void ResetGame();
}