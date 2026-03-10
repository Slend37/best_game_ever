using BestGameEver.Core;

namespace BestGameEver.Gameplay;

public class NormalApple : IApple
{
    public int Value {get; private set;}

    public NormalApple(int value)
    {
        if (value < 1)
            throw new ArgumentException("Golden apple value must be greater than 0");
        Value = value;
    }

    public void Get(Snake snake)
    {
        snake.Size += Value;
    }
}