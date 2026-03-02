using System.ComponentModel;
using System.Data;
using System.Dynamic;
using System.Runtime.CompilerServices;

namespace BestGameEver;

public class Game
{
    public int MapWidth{ get; private set; } = 100;
    public int MapHeight{ get; private set; } = 100;

    public string Difficulty{ get; private set; } = "Easy";

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
        Console.Clear();
        Console.WriteLine($"Game started with difficulty: [{Difficulty}]");
    }
    public void Run()
    {
        while (!GameIsEnded())
        {
            HandleInput();
            Update();
            Render();
        }
    }

    private bool GameIsEnded()
    {
        return gameStopped;
    }

    private void HandleInput()
    {
        var key = Console.ReadKey(true);
        if (key.Key == ConsoleKey.Escape)
            gameStopped = true;
    }
    private void Update()
    {
        
    }

    private void Render()
    {
        Console.Clear();
        Console.WriteLine("Game is running...");
    }

    private bool gameStopped = false;
    private static Game? instance;
}