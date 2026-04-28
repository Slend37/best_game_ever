// GameFacade.cs
using BestGameEver.Core;
using BestGameEver.Builders;
using BestGameEver.Core.Components;

namespace BestGameEver;

public class GameFacade
{
    private readonly IGameRenderer _renderer;
    private readonly IGameInputHandler _inputHandler;
    private readonly IGameStateManager _stateManager;
    private readonly IGameLoop _gameLoop;
    
    private Level _level;
    private Snake _snake;
    private char[,] _drawBuffer;
    
    private readonly int _mapWidth;
    private readonly int _mapHeight;
    
    public GameFacade(
        IGameRenderer renderer,
        IGameInputHandler inputHandler,
        IGameStateManager stateManager,
        IGameLoop gameLoop,
        int mapWidth = 40,
        int mapHeight = 15)
    {
        _renderer = renderer;
        _inputHandler = inputHandler;
        _stateManager = stateManager;
        _gameLoop = gameLoop;
        _mapWidth = mapWidth;
        _mapHeight = mapHeight;
    }
    
    public void StartGame()
    {
        InitializeGame();
        _gameLoop.Start();
        
        while (_stateManager.IsGameRunning)
        {
            _gameLoop.Update();
            ProcessInput();
            UpdateGame();
            RenderGame();
        }
        
        Cleanup();
    }
    
    private void InitializeGame()
    {
        _renderer.Clear();
        _drawBuffer = new char[_mapHeight, _mapWidth];
        _level = new Level(_mapWidth, _mapHeight);
        _snake = new SnakeBuilder()
            .SetSize(3)
            .SetProtection(false)
            .SetProtectionTime(0)
            .SetPosition(new Position(5, 5))
            .SetWinSize(8)
            .SetDirection(Direction.Right)
            .Build();
    }
    
    private void ProcessInput()
    {
        var key = _inputHandler.GetKey();
        
        if (_inputHandler.IsExitKey(key))
        {
            _stateManager.StopGame();
            return;
        }
        
        switch (key)
        {
            case ConsoleKey.UpArrow:
                if (_level.CanMoveTo(_snake.Position.Up()))
                    _snake.Move(Direction.Up);
                break;
            case ConsoleKey.RightArrow:
                if (_level.CanMoveTo(_snake.Position.Right()))
                    _snake.Move(Direction.Right);
                break;
            case ConsoleKey.DownArrow:
                if (_level.CanMoveTo(_snake.Position.Down()))
                    _snake.Move(Direction.Down);
                break;
            case ConsoleKey.LeftArrow:
                if (_level.CanMoveTo(_snake.Position.Left()))
                    _snake.Move(Direction.Left);
                break;
        }
    }
    
    private void UpdateGame()
    {
        Cell curr = _level.GetCell(_snake.Position);
        foreach (var apple in curr.Apples)
            apple.Get(_snake);
        
        curr.RemoveAllApples();
    }
    
    private void RenderGame()
    {
        ClearDrawBuffer();
        DrawLevel();
        DrawSnake();
        
        _renderer.Clear();
        _renderer.Draw(_drawBuffer);
        
        string status = $"Size: {_snake.Size} | Protection: {_snake.ProtectTime}s | Win size: {_snake.WinSize}";
        _renderer.DrawStatus(status);
    }
    
    private void ClearDrawBuffer()
    {
        for (int line = 0; line < _mapHeight; ++line)
            for (int column = 0; column < _mapWidth; ++column)
                _drawBuffer[line, column] = ' ';
    }
    
    private void DrawLevel()
    {
        foreach (var cell in _level.Cells)
        {
            if (!cell.IsPassable)
                _drawBuffer[cell.Position.Line, cell.Position.Column] = '#';
            else if (cell.Apples.Any())
                _drawBuffer[cell.Position.Line, cell.Position.Column] = '*';
        }
    }
    
    private void DrawSnake()
    {
        _drawBuffer[_snake.Position.Line, _snake.Position.Column] = '@';
    }
    
    private void Cleanup()
    {
        _gameLoop.Stop();
        Console.Clear();
        Console.WriteLine("Game Over! Press any key to exit...");
        Console.ReadKey();
        Console.CursorVisible = true;
    }
}