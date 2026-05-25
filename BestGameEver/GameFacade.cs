// GameFacade.cs
using BestGameEver.Core;
using BestGameEver.Builders;
using BestGameEver.Core.Components;
using System.Timers;

namespace BestGameEver;

public class GameFacade
{
    private readonly IGameRenderer _renderer;
    private readonly IGameInputHandler _inputHandler;
    private readonly IGameStateManager _stateManager;
    private readonly IGameLoop _gameLoop;
    private readonly KeyRemapper _keyRemapper;

    private System.Timers.Timer _gameTimer;
    private Level _level;
    private Snake _snake;
    private char[,] _drawBuffer;
    
    private readonly int _mapWidth;
    private readonly int _mapHeight;
    private int _gameSpeed;
    private bool _isPaused;
    private int curLevel = 0;
    private Direction blockDirection = Direction.Right;
    
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
        
        _keyRemapper = new KeyRemapper(renderer, inputHandler);
        
        _keyRemapper.OnKeyRemapped += OnKeyRemapped;
        
        _gameSpeed = 500;
        _isPaused = false;
    }
    
    private void OnKeyRemapped(Direction direction, ConsoleKey newKey)
    {
        _renderer.DrawText($"Movement for {direction} is now bound to {newKey}");
        System.Threading.Thread.Sleep(800);
        
        ResumeGame();
    }
    
    public void StartGame()
    {
        InitializeGame();
        _gameLoop.Start();
        
        SetupGameTimer();
        
        while (_stateManager.IsGameRunning)
        {
            _gameLoop.Update();
            ProcessInput();
            Thread.Sleep(10);
            
            if (!_isPaused)
            {
                UpdateGame();
            }
            
            RenderGame();
        }
        
        Cleanup();
    }

    private void SetupGameTimer()
    {
        _gameTimer = new System.Timers.Timer(_gameSpeed);
        _gameTimer.Elapsed += OnGameTick;
        _gameTimer.AutoReset = true;
        _gameTimer.Enabled = true;
    }

    private void NextLevel()
    {
        int levelSize = _level.GetApplesCount() + 3;
        if (_snake.Size == (levelSize / 2) && curLevel == 0)
        {
            _gameTimer.Stop();
            _gameTimer.Dispose();
            _gameSpeed = 350;
            SetupGameTimer();
            curLevel = 1;
        }

        if (_snake.Size == (levelSize - levelSize / 4) && curLevel == 1)
        {
            _gameTimer.Stop();
            _gameTimer.Dispose();
            _gameSpeed = 250;
            SetupGameTimer();
            curLevel = 2;
        }

        if (_snake.Size + 1 == levelSize)
        {
            _gameTimer.Stop();
            _gameTimer.Dispose();
            _gameSpeed = 150;
            SetupGameTimer();
            curLevel = 3;
        }
    }

    private void OnGameTick(object sender, ElapsedEventArgs e)
    {
        NextLevel();
        if (!_stateManager.IsGameRunning || _isPaused)
            return;
            
        _snake._SetDirection(blockDirection);
        _snake.Move(_snake.Direction);
        
        if (!_level.CanMoveTo(_snake.Position))
        {
            GameOver("You hit the wall!");
            return;
        }
        
        if (_snake.Body.IsHeadCollidingWithBody())
        {
            GameOver("You collided with yourself!");
            return;
        }
    }
    
    private void GameOver(string reason)
    {
        _stateManager.StopGame();
        _renderer.Clear();
        _renderer.DrawText($"GAME OVER! {reason}");
        _renderer.DrawText($"Your final score: {_snake.Size}");
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
            .SetWinSize(_level.GetApplesCount() + 3)
            .SetDirection(Direction.Right)
            .SetBody()
            .Build();
    }
    
    private void ProcessInput()
    {
        while (_inputHandler.HasKey())
        {
            var key = _inputHandler.GetKey();
        
            if (_inputHandler.IsExitKey(key))
            {
                _stateManager.StopGame();
                return;
            }
            
            if (_keyRemapper.IsRemapKey(key) && !_isPaused)
            {
                PauseGame();
                _keyRemapper.OpenRemappingMenu();
                RenderGame();
                continue;
            }
            
            if (_keyRemapper.IsInRemappingMode())
            {
                _keyRemapper.ProcessRemappingInput(key);
                RenderGame();
                continue;
            }
            
            if (_isPaused)
                continue;
            
            if (_keyRemapper.TryGetDirection(key, out var requestedDirection))
            {
                if (IsOppositeDirection(requestedDirection, _snake.Direction))
                    continue;
                
                var newPosition = GetPositionInDirection(_snake.Position, requestedDirection);
                if (_level.CanMoveTo(newPosition))
                {
                    blockDirection = requestedDirection;
                }
            }
        }
    }
    
    private void PauseGame()
    {
        _isPaused = true;
        _gameTimer.Stop();
    }
    
    private void ResumeGame()
    {
        _isPaused = false;
        _gameTimer.Start();
    }
    
    private bool IsOppositeDirection(Direction requested, Direction current)
    {
        return (requested == Direction.Up && current == Direction.Down) ||
               (requested == Direction.Down && current == Direction.Up) ||
               (requested == Direction.Left && current == Direction.Right) ||
               (requested == Direction.Right && current == Direction.Left);
    }
    
    private Position GetPositionInDirection(Position pos, Direction direction)
    {
        return direction switch
        {
            Direction.Up => pos.Up(),
            Direction.Down => pos.Down(),
            Direction.Left => pos.Left(),
            Direction.Right => pos.Right(),
            _ => pos
        };
    }
    
    private void UpdateGame()
    {
        Cell currentCell = _level.GetCell(_snake.Position);
        
        foreach (var apple in currentCell.Apples)
        {
            apple.Get(_snake);
        }
        
        currentCell.RemoveAllApples();
        
        if (_snake.Size >= _snake.WinSize)
        {
            WinGame();
        }
    }
    
    private void WinGame()
    {
        _stateManager.StopGame();
        _renderer.DrawText($"YOU WIN! Congratulations!");
        _renderer.DrawText($"Final size: {_snake.Size}");
        _renderer.DrawText("Press any key to continue...");
    }
    
    private void RenderGame()
    {
        ClearDrawBuffer();
        DrawLevel();
        DrawSnake();
        
        _renderer.Clear();
        _renderer.Draw(_drawBuffer);
        
        var bindings = _keyRemapper.GetCurrentBindings();
        string status = $"Size: {_snake.Size} | Win: {_snake.WinSize} | ";
        status += $"Controls: [{bindings[Direction.Up]}/{bindings[Direction.Down]}/{bindings[Direction.Left]}/{bindings[Direction.Right]}] ";
        status += $"| Press R to remap keys";
        
        if (_isPaused)
        {
            status += " | PAUSED - Remapping mode";
        }
        
        _renderer.DrawStatus(status);
        
        if (_isPaused)
        {
            _renderer.DrawText("");
            _renderer.DrawText("=== REMAPPING MODE ===");
            _renderer.DrawText("Game is paused while you customize controls");
            _renderer.DrawText("Follow the instructions above to remap keys");
        }
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
        var positions = _snake.Body.GetAllPositions().ToList();
        for (int i = 0; i < positions.Count; i++)
        {
            char symbol = (i == 0) ? '@' : 'o';
            _drawBuffer[positions[i].Line, positions[i].Column] = symbol;
        }
    }
    
    private void Cleanup()
    {
        _gameLoop.Stop();
        _gameTimer?.Stop();
        _gameTimer?.Dispose();
        
        Console.Clear();
        if (_snake.Size >= _snake.WinSize)
        {
            WinGame();
        }
        else
        {
            Console.WriteLine($"Game Over! Your final score: {_snake.Size}");
            Console.WriteLine("Press any key to exit...");
        }
        Console.ReadKey();
        Console.CursorVisible = true;
    }
}