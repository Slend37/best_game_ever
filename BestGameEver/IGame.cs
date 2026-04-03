namespace BestGameEver;

public interface IGame
{
    bool IsGameEnded();
    void HandleInput();
    void Update();
    void Render();
}