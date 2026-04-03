using BestGameEver.Core;
using BestGameEver.Builders;


namespace BestGameEver
{
    class Program
    {
        static void Main(string[] args)
        {
            IGame game = Game.Instance;
            var gameLoopFacade = new GameLoopFacade(game);

            gameLoopFacade.Run();

            // Console.WriteLine("Starting...");
            // Game.Instance.Run();

            /*
            var snake = new SnakeBuilder()
                .SetSize(3)
                .SetProtectionTime(0)
                .Build();

            ISnakeStats stats = new BaseSnakeStats(snake);
            stats = new SizeBonusDecorator(new ProtectTimeBonusDecorator(new SizePenaltyDecorator(stats, 2), 10), 5);

            Console.WriteLine($"Итоговый размер: {stats.GetSize()}");
            Console.WriteLine($"Итоговое время защиты: {stats.GetProtectTime()}");
            */
        }
    }
}