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
    }
}