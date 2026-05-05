using BestGameEver.Core;

namespace BestGameEver.Gameplay;

public class GoldenApple : IApple
{
    public int Value_ {get; private set;}
    public int Time {get; private set; }

    public GoldenApple(int value, int time)
    {
        if (value < 1)
            throw new ArgumentException("Golden apple value must be greater than 0");
        if (time < 0)
            throw new ArgumentException("Time can not be negative");

        Value_ = value;
        Time = time;
        
    }

    public void Get(Snake snake)
    {
        snake.Size += Value_;
        snake.Protect = true;
        snake.ProtectTime += Time;
        Position maybe = new Position(snake.Body.GetPositionAt(snake.Body.Count-1).Line - 1, snake.Body.GetPositionAt(snake.Body.Count-1).Column);
        if (snake.Body.GetAllPositions().Contains(maybe)){
            maybe = new Position(snake.Body.GetPositionAt(snake.Body.Count-1).Line, snake.Body.GetPositionAt(snake.Body.Count-1).Column - 1);
            if (snake.Body.GetAllPositions().Contains(maybe))
            {
                maybe = new Position(snake.Body.GetPositionAt(snake.Body.Count-1).Line + 1, snake.Body.GetPositionAt(snake.Body.Count-1).Column);
                if (snake.Body.GetAllPositions().Contains(maybe))
                {
                    maybe = new Position(snake.Body.GetPositionAt(snake.Body.Count-1).Line, snake.Body.GetPositionAt(snake.Body.Count-1).Column + 1);
                }
            }
        } 
        snake.Body.AddPosition(maybe);
    }
}