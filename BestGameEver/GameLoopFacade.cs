namespace BestGameEver;

public class GameLoopFacade
{
    private readonly IGame _game;

    public GameLoopFacade(IGame game)
    {
        _game = game;
    }

    public void Run()
    {
        while (!_game.IsGameEnded())
        {
            _game.HandleInput();
            _game.Update();
            _game.Render();
        }
    }
}