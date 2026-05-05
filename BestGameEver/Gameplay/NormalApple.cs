using BestGameEver.Core;

namespace BestGameEver.Gameplay;

public class NormalApple : IApple
{
    public int Value_ {get; private set;}

    public NormalApple(int value)
    {
        if (value < 1)
            throw new ArgumentException("Golden apple value must be greater than 0");
        Value_ = value;
    }

    public void Get(Snake snake)
    {
        snake.Size += Value_;
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