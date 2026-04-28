namespace BestGameEver.Core.Components;
public class GameStateManager : IGameStateManager
{
    public bool IsGameRunning { get; private set; } = true;
    
    public void StopGame()
    {
        IsGameRunning = false;
    }
    
    public void ResetGame()
    {
        IsGameRunning = true;
    }
}