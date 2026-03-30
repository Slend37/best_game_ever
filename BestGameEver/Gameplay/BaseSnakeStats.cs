namespace BestGameEver.Core
{
    public class BaseSnakeStats : ISnakeStats
    {
        private readonly Snake snake;
        public BaseSnakeStats(Snake snake) => this.snake = snake;

        public int GetSize() => snake.Size;
        public int GetProtectTime() => snake.ProtectTime;
    }
}