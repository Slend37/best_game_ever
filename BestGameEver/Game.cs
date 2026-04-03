using BestGameEver.Core;
using BestGameEver.Builders;

namespace BestGameEver;

public class Game : IGame
{
    public int MapWidth{ get; private set; }
    public int MapHeight{ get; private set; }


    public static Game Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Game();
                instance.Init();
            }
            return instance;
        }
    }

    public void Init()
    {
        gameStopped = false;
        Console.Clear();
    }
    public void Run()
    {
        while (!IsGameEnded())
        {
            HandleInput();
            Update();
            Render();
        }
    }

    public bool IsGameEnded()
    {
        return gameStopped;
    }

    public void HandleInput()
    {
        var key = Console.ReadKey(true);
        switch (key.Key)
        {
            case ConsoleKey.Escape:
                gameStopped = true;
                break;
            case ConsoleKey.UpArrow:
                if (level.CanMoveTo(snake.Position.Up()))
                    snake.Move(Direction.Up);
                break;
            case ConsoleKey.RightArrow:
                if(level.CanMoveTo(snake.Position.Right()))
                    snake.Move(Direction.Right);
                break;
            case ConsoleKey.DownArrow:
                if (level.CanMoveTo(snake.Position.Down()))
                    snake.Move(Direction.Down);
                break;
            case ConsoleKey.LeftArrow:
                if (level.CanMoveTo(snake.Position.Left()))
                    snake.Move(Direction.Left);
                break;
        }
    }
    public void Update()
    {
        Cell curr = level.GetCell(snake.Position);
        foreach(var apple in curr.Apples)
            apple.Get(snake);
        
        curr.RemoveAllApples();
    }

    public void Render()
    {
        Console.Clear();
        ClearDrawBuffer();
        DrawLevel();
        DrawSnake();
        Flush();
        DrawStatus();
    }
    private void DrawStatus()
    {
        Console.WriteLine($"Size: {snake.Size}");
        Console.WriteLine($"Protection time: {snake.ProtectTime}");
        Console.WriteLine($"Win size: {snake.WinSize}");
    }
    
    private Game()
    {
        MapHeight = 15;
        MapWidth = 40;
        Console.WriteLine("Game...");
        drawBuffer = new char[MapHeight, MapWidth];
        Console.WriteLine("Char...");
        level = new Level(MapWidth, MapHeight);
        Console.WriteLine("Level...");
        snake = new SnakeBuilder()
            .SetSize(3)
            .SetProtection(false)
            .SetProtectionTime(0)
            .SetPosition(new Position(5, 5))
            .SetWinSize(8)
            .SetDirection(Direction.Right)
            .Build();
        Console.WriteLine("Snake...");

        gameStopped = false;
    }

    private void ClearDrawBuffer()
    {
        for(int line = 0; line < MapHeight; ++line)
        {
            for(int column = 0; column < MapWidth; ++column)
            {
                drawBuffer[line, column] = ' ';
            }
        }
    }

    private void DrawLevel()
    {
        foreach(var cell in level.Cells)
        {
            if (!cell.IsPassable)
                drawBuffer[cell.Position.Line, cell.Position.Column] = '#';
            else if(cell.Apples.Any())
                drawBuffer[cell.Position.Line, cell.Position.Column] = '*';
        }
    }

    private void DrawSnake()
    {
        drawBuffer[snake.Position.Line, snake.Position.Column] = '@';
    }    

    private void Flush()
    {
        for(int line = 0; line < MapHeight; ++line)
        {
            for(int column = 0; column < MapWidth; ++column)
            {
                if(line < Console.BufferHeight && column < Console.BufferWidth - 1)
                    Console.Write(drawBuffer[line, column]);
            }
            Console.WriteLine();
        }
    }

    private bool gameStopped;
    private static Game? instance;
    private readonly Level level;
    private readonly Snake snake;
    private readonly char[,] drawBuffer;
}