// Program.cs
using BestGameEver.Core;
using BestGameEver;
using BestGameEver.Core.Components;

namespace BestGameEver
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.Title = "Snake Game";
            Console.WindowWidth = 45;
            Console.WindowHeight = 20;
            
            var renderer = new ConsoleRenderer(40, 15);
            var inputHandler = new ConsoleInputHandler();
            var stateManager = new GameStateManager();
            var gameLoop = new SimpleGameLoop();
            
            var game = new GameFacade(
                renderer,
                inputHandler,
                stateManager,
                gameLoop,
                mapWidth: 40,
                mapHeight: 15
            );
            
            game.StartGame();
        }
    }
}